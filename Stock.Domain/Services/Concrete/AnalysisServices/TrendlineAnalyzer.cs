﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Services
{

    public class TrendlineAnalyzer : Analyzer, ITrendlineAnalyzer
    {
        private const int OppositeExtremaMinDistance = 10;
        private ITrendlineProcessor processor;

        private const int RangeToCheck = 200;
        private const int MinDistance = 5;

        private IEnumerable<ExtremumGroup> extremaGroups;
        private IEnumerable<Trendline> trendlines = new List<Trendline>();


        public override AnalysisType Type
        {
            get { return AnalysisType.Trendline; }
        }


        public TrendlineAnalyzer(AssetTimeframe atf) : base(atf)
        {
        }


        protected override void initialize_specific()
        {
            ItemsForAnalysis = 300;
        }

        protected override IAnalyzerProcessor getProcessor()
        {
            ITrendlineAnalyzer analyzer = this;
            if (processor == null) processor = new TrendlineProcessor(analyzer);
            return processor;
        }


        private void preAnalysisStaff(DataItem[] items)
        {

            //Create [TrendlineProcessor] instance.
            if (processor == null)
            {
                ITrendlineAnalyzer self = this;
                processor = new TrendlineProcessor(self);
            }

            if (trendlines == null)
            {
                this.trendlines = new List<Trendline>();
            }


            /* Extract items that are marked as extrema. */
            this.extremaGroups = items.Where(i => i.Price != null && i.Price.IsExtremum()).OrderBy(i => i.Date).GetExtremaGroups().OrderBy(i => i.getDate()); ;
            

        }



        public override void Analyze(DataItem[] items)
        {

            preAnalysisStaff(items);

            IEnumerable<ExtremumGroup> newGroups = this.extremaGroups.Where(i => i.getDate().CompareTo(LastCalculationDate) > 0);

            foreach (var extremum in newGroups)
            {

                /* Iterate through all the groups above and check trendlines for each pair of extrema. */
                var previous = this.extremaGroups.Where(i => i.getDate() < extremum.getDate() && 
                                    i.getDate() > extremum.getDate().addTimeUnits(this.AssetTimeframe.timeframe.Symbol, -ItemsForAnalysis));

                foreach (var subextremum in previous)
                {

                    if (Math.Abs(extremum.master.Distance(subextremum.master)) > RangeToCheck) break;
                    //If items are of opposite type, there must be more distance between them.
                    if (extremum.type.IsOpposite(subextremum.type) && Math.Abs(extremum.master.Distance(subextremum.master)) < OppositeExtremaMinDistance) break;
                    var trendlines = ProcessSinglePair(extremum, subextremum);
                    foreach (var trendline in trendlines)
                    {
                        trendlines.Add(trendline);
                    }

                }

            }

            //processor.Analyze(items);
        }


        public List<Trendline> ProcessSinglePair(ExtremumGroup extremum, ExtremumGroup subextremum)
        {

            List<Trendline> trendlines = new List<Trendline>();


            ///* 
            // *  Check if the space between those extrema is not less than minimum distance, defined by [MinDistance] const.
            // *  If it is less than [MinDistance], it means those extrema lie to close to each other and are probably parts
            // *  of the same bigger Peak/Trough.
            // */
            //if (Math.Abs(extremum.Distance(subextremum)) < MinDistance) return trendlines;


            ///*
            // *  If there are opposite extrema (peak/trough) check if they can be processed.
            // *  In order to process the following pair: troughs are checked only if flag [isAbove] = true, 
            // *  peaks if [isAbove] = false;
            // */
            //if ((extremum.Price.IsPeak() && subextremum.Price.IsTrough()) || (extremum.Price.IsTrough() && subextremum.Price.IsPeak()))
            //{
            //    var midextremum = extremaGroups.Where(e => e.master.Price != null && e.master.Date > extremum.Date && e.master.Date < subextremum.Date && e.master.Price.IsPeak() == extremum.Price.IsPeak()).ToArray();
            //    if (midextremum.Length == 0)
            //    {
            //        return trendlines;
            //    }
            //}



            ///*
            // *  If the tests above are passed, we can continue with calculating relevance of each trendline variant for this pair.
            // */
            //for (var a = extremum.GetProperOpenOrClose(); a <= extremum.GetProperHighOrLow(); a += extremum.TrendlineAnalysisStep())
            //{
            //    for (var b = subextremum.GetProperOpenOrClose(); b <= subextremum.GetProperHighOrLow(); b += subextremum.TrendlineAnalysisStep())
            //    {

            //        var trendline = processor.Analyze(extremum, a, subextremum, b);
            //        if (trendline != null) trendlines.Add(trendline);

            //    }

            //}


            //return FilterTrendlines(trendlines);

            return null;

        }




        public List<Trendline> FilterTrendlines(List<Trendline> trendlines)
        {

            var result = new List<Trendline>();

            //Podziel linie na kategorie wg HitsMD5.
            Dictionary<string, List<Trendline>> groups = new Dictionary<string, List<Trendline>>();
            foreach (var trendline in trendlines)
            {
                var md5 = trendline.HitsHashCode;
                List<Trendline> group = null;
                groups.TryGetValue(md5, out group);

                if (group == null)
                {
                    group = new List<Trendline>();
                    groups.Add(md5, group);
                }

                group.Add(trendline);

            }

            //Z każdej grupy pozostaw tylko linię z najlepszą punktacją (+ ewentualnie linie przekraczające określony poziom).

            foreach (var group in groups.Values)
            {

                var best = group.Max(t => t.Score);
                var ordered = group.Where(t => t.Score >= 0.9d * best).OrderByDescending(t => t.Score).ToArray();

                foreach (var trendline in ordered)
                {
                    result.Add(trendline);
                }

            }

            return result;

        }


    }
}
