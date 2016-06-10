using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Services.Factories;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;


namespace Stock.Domain.Services
{
    public class TrendService : ITrendService
    {
        /* Config parameters. */
        private const int RangeToCheck = 200;
        private const int MinDistance = 5;

        /* Process properties */
        private Asset Asset;
        private Timeframe Timeframe;
        private string Symbol;

        /* Data collections */
        private DataItem[] Items;
        private DataItem[] Extrema;

        /* Result collection */
        private List<Trendline> Trendlines = new List<Trendline>();

        /* Processors */
        private ITrendlineAnalyzer analyzer;


        #region CONSTRUCTORS
        public TrendService(Asset asset, Timeframe timeframe)
        {
            LoadProperties(asset, timeframe);
        }

        public TrendService(string symbol)
        {
            LoadProperties(symbol);
        }

        public TrendService(Asset asset, Timeframe timeframe, DataItem[] items)
        {
            LoadProperties(asset, timeframe);
            LoadItems(items);
        }

        public TrendService(string symbol, DataItem[] items)
        {
            LoadProperties(symbol);
            LoadItems(items);
        }
        #endregion

        #region GETTER_METHODS
        public DataItem[] GetItems()
        {
            return Items;
        }

        public DataItem[] GetExtrema()
        {
            return Extrema;
        }

        public Trendline[] GetTrendlines()
        {
            return Trendlines.ToArray();
        }

        #endregion



        private void LoadProperties(string symbol)
        {
            this.Symbol = symbol;

            var names = symbol.Split('_');
            this.Asset = MarketService.Instance().GetFxPair(names[0]);
            this.Timeframe = Timeframe.GetTimeframeByShortName(names[1]);
            this.analyzer = new TrendlineAnalyzer(Asset, Timeframe);

        }

        private void LoadProperties(Asset asset, Timeframe timeframe)
        {

            this.Asset = asset;
            this.Timeframe = timeframe;
            this.Symbol = asset.Name + "_" + timeframe.Name;
            this.analyzer = new TrendlineAnalyzer(Asset, Timeframe);

        }

        public void LoadItems(DataItem[] items)
        {

            this.Items = items;
            this.Items.AppendIndexNumbers();

            /* Extract items that are marked as extrema. */
            this.Extrema = items.Where(i => i.Price != null && i.Price.IsExtremum()).OrderBy(i => i.Date).ToArray();


            /* Load those items also to [TrendlineAnalyzer] if it exists. */
            if (analyzer != null)
            {
                analyzer.LoadItems(this.Items);
            }

        }



        public Trendline[] Start()
        {
            
            foreach (var extremum in Extrema)
            {


                /* Iterate through all the groups above and check trendlines for each pair of extrema. */
                var subextrema = Extrema.Where(i => i.Date > extremum.Date).ToArray();
                foreach (var subextremum in subextrema)
                {

                    if (Math.Abs(extremum.Distance(subextremum)) > RangeToCheck) break;

                    var trendlines = ProcessSinglePair(extremum, subextremum);
                    foreach (var trendline in trendlines)
                    {
                        Trendlines.Add(trendline);
                    }

                }

            }


            return this.Trendlines.ToArray();

        }


        public List<Trendline> ProcessSinglePair(DataItem extremum, DataItem subextremum)
        {

            List<Trendline> trendlines = new List<Trendline>();


            /* 
             *  Check if the space between those extrema is not less than minimum distance, defined by [MinDistance] const.
             *  If it is less than [MinDistance], it means those extrema lie to close to each other and are probably parts
             *  of the same bigger Peak/Trough.
             */
            if (Math.Abs(extremum.Distance(subextremum)) < MinDistance) return trendlines;


            /*
             *  If there are opposite extrema (peak/trough) check if they can be processed.
             *  In order to process the following pair: troughs are checked only if flag [isAbove] = true, 
             *  peaks if [isAbove] = false;
             */
            if ((extremum.Price.IsPeak() && subextremum.Price.IsTrough()) || (extremum.Price.IsTrough() && subextremum.Price.IsPeak()))
            {
                var midextremum = Extrema.Where(e => e.Price != null && e.Date > extremum.Date && e.Date < subextremum.Date && e.Price.IsPeak() == extremum.Price.IsPeak()).ToArray();
                if (midextremum.Length == 0)
                {
                    return trendlines;
                }
            }



            /*
             *  If the tests above are passed, we can continue with calculating relevance of each trendline variant for this pair.
             */
            for (var a = extremum.GetProperOpenOrClose(); a <= extremum.GetProperHighOrLow(); a += extremum.TrendlineAnalysisStep())
            {
                for (var b = subextremum.GetProperOpenOrClose(); b <= subextremum.GetProperHighOrLow(); b += subextremum.TrendlineAnalysisStep())
                {

                    var trendline = analyzer.Analyze(extremum, a, subextremum, b);
                    if (trendline != null) trendlines.Add(trendline);

                }
            }



            return FilterTrendlines(trendlines);


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
                
                foreach(var trendline in ordered){
                    result.Add(trendline);
                }

            }

            return result;

        }


    }
}
