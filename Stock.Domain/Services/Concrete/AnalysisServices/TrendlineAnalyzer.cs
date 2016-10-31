using System;
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
        private const int MinDistance = 5;
        private ITrendlineProcessor processor;

        private const int RangeToCheck = 200;

        private IEnumerable<ExtremumGroup> extremaGroups;
        private List<Trendline> trendlines = new List<Trendline>();
        private Dictionary<string, Trendline> optimalTrendlines = new Dictionary<string, Trendline>();


        public override AnalysisType Type
        {
            get { return AnalysisType.Trendline; }
        }


        public TrendlineAnalyzer(AssetTimeframe atf) : base(atf)
        {
            trendlines = new List<Trendline>();
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

                    //Items must have minimum distance between each other.
                    if (Math.Abs(extremum.master.Distance(subextremum.master)) < MinDistance) break;
                    if (extremum.type.IsOpposite(subextremum.type) && Math.Abs(extremum.master.Distance(subextremum.master)) < OppositeExtremaMinDistance) break;


                    //Do tego momentu docierają tylko te pary wierzchołków, które spełniają kryteria początkowe.



                    //Do zmiennej pairTrendlines przypisywana jest kolekcja wszystkich kombinacji trendlinów stworzonych dla aktualnie rozpatrywanej pary wierzchołków.
                    var pairTrendlines = ProcessSinglePair(subextremum, extremum, items);

                    //Zapisać zwrócone linie trendu do bazy.

                    //Potem dodać do listy [trendlines].
                    this.trendlines.AddRange(pairTrendlines);

                    //wybrać najlepszy i dodać do optymalnych.
                    foreach (var trendline in pairTrendlines)
                    {
                        addOptimalTrendline(trendline);
                        break;
                    }

                }

            }

            //processor.Analyze(items);
        }




        public List<Trendline> ProcessSinglePair(ExtremumGroup extremum, ExtremumGroup subextremum, DataItem[] items)
        {

            List<Trendline> trendlines = new List<Trendline>();


            /*
             *  If there are opposite extrema (peak/trough) check if they can be processed.
             *  There must be at least one other extremum between them.
             *  In order to process the following pair: troughs are checked only if flag [isAbove] = true, 
             *  peaks if [isAbove] = false;
             */
            if ((extremum.type.IsPeak() && !subextremum.type.IsPeak()) || (!extremum.type.IsPeak() && subextremum.type.IsPeak()))
            {
                var midextremum = extremaGroups.Where(e => e.master.Price != null && e.master.Date > extremum.master.Date && e.master.Date < subextremum.master.Date && e.type.IsPeak() == extremum.type.IsPeak()).ToArray();
                if (midextremum.Length == 0)
                {
                    return trendlines;
                }
            }



            /*
             *  If the tests above are passed, we can continue with calculating relevance of each trendline variant for this pair.
             */
            for (var a = extremum.getLower(); a <= extremum.getHigher(); a += extremum.getStep())
            {
                for (var b = subextremum.getLower(); b <= subextremum.getHigher(); b += subextremum.getStep())
                {
                    var trendline = new Trendline(this.AssetTimeframe, new ValuePoint(extremum.getStartItem(), a), new ValuePoint(subextremum.getEndItem(), b));
                    processor.Analyze(trendline, items, trendline.LastAnalyzed);

                    if (trendline.IsImportant())
                    {

                        if (trendlines.Count == 0)
                        {
                            trendlines.Add(trendline);
                        }

                    }

                }

            }


            //return FilterTrendlines(trendlines);
            return trendlines;

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

        private void addOptimalTrendline(Trendline trendline)
        {
            this.optimalTrendlines.Add(trendline.HitsHashCode, trendline);
        }

        public IEnumerable<Trendline> GetTrendlines()
        {
            return trendlines;
        }

    }
}
