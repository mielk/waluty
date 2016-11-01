using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.Domain.Services;

namespace Stock.Domain.Entities
{

    public class Trendline
    {

        public int Id { get; set; }
        public AssetTimeframe AssetTimeframe { get; set; }
        public ValuePoint InitialPoint { get; set; }
        public ValuePoint BoundPoint { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsFinished { get; set; }
        public double Slope { get; set; }
        public double Score { get; set; }
        public string HitsHashCode { get; set; }
        public Guid Uid { get; set; }
        public List<TrendHit> Hits { get; set; }
        public List<TrendBreak> Breaks { get; set; }
        public List<TrendBounce> Bounces { get; set; }
        public TrendlineType CurrentType { get; set; }
        public DataItem PreviousHit { get; set; }
        public DataItem PreviousBreak { get; set; }
        private double TotalOverBreak;
        private IPriceTrendComparer extremumComparer = new ExtremumPriceTrendComparer();
        private IPriceTrendComparer otherComparer = new PriceTrendComparer();
        public DataItem LastAnalyzed { get; set; }

        //Analyse-purpose variables.
        public TrendHit currentHit { get; set; }
        public TrendBounce currentBounce { get; set; }


        public Trendline(int id, AssetTimeframe atf, ValuePoint initial, ValuePoint bound)
        {
            initialize();
            this.Id = id;
            assignProperties(atf, initial, bound);
        }

        public Trendline(AssetTimeframe atf, ValuePoint initial, ValuePoint bound)
        {
            initialize();
            assignProperties(atf, initial, bound);
        }


        public Asset asset()
        {
            return this.AssetTimeframe.asset;
        }

        public Timeframe timeframe()
        {
            return this.AssetTimeframe.timeframe;
        }


        private void initialize()
        {
            Uid = System.Guid.NewGuid();
            Hits = new List<TrendHit>();
            Bounces = new List<TrendBounce>();
            Breaks = new List<TrendBreak>();
           
        }


        private void assignProperties(AssetTimeframe atf, ValuePoint initial, ValuePoint bound)
        {
            this.AssetTimeframe = atf;
            this.InitialPoint = initial;
            this.BoundPoint = bound;
            this.Slope = (bound.value - initial.value) / (BoundPoint.index() - InitialPoint.index());
            this.CurrentType = (initial.dataItem.Price.IsPeak() ? TrendlineType.Resistance : TrendlineType.Support);

        }






        public override string ToString()
        {
            return InitialPoint.dataItem.Date.ToString() + "; " +
                    InitialPoint.value + "; " +
                    BoundPoint.dataItem.Date.ToString() + "; " +
                    BoundPoint.value + "; " +
                    Slope + "; " +
                    Hits.Count + "; " +
                    HitsHashCode + "; " +
                    Score + "; ";
        }



        /* Funkcja zwracająca poziom na którym znajduje się ta linia trendu
         * dla podanego indeksu. */
        public double GetLevel(int index)
        {
            return (index - this.InitialPoint.index()) * this.Slope + this.InitialPoint.value;
        }


        public bool IsAscending()
        {
            return (Slope > 0);
        }


        public void RecalculateScore()
        {
            
            Score = calculateScore();
            HitsHashCode = calculateHitsMD5();

        }


        private double calculateScore()
        {
            var hitAverage = Hits.Average(h => h.Score);
            return hitAverage * Math.Sqrt(Hits.Count);
        }

        public bool IsImportant()
        {
            return true;
        }




        public void setNewHit(DataItem item)
        {

            TrendHit hit = new TrendHit(this, item, currentHit, currentBounce);
            TrendBounce bounce = new TrendBounce(this, hit);
            hit.BounceToNextHit = bounce;


            //Close current hit and bounce.
            if (currentHit != null)
            {
                currentHit.NextHit = hit;
                this.Hits.Add(currentHit);
            }
            if (currentBounce != null)
            {
                currentBounce.EndHit = hit;
                this.Bounces.Add(currentBounce);
            }

            currentHit = hit;
            currentBounce = bounce;

            //Calculate points.
            hit.Calculate();
            bounce.Calculate();
            
        }


        public void setNewBreak(DataItem item)
        {
            TrendBreak tBreak = new TrendBreak();
            tBreak.Trendline = this;
            tBreak.TrendBounce = currentBounce;
            tBreak.TrendLevel = this.GetLevel(item.Index);
            tBreak.Item = item;
            
            currentBounce.AddBreak(tBreak);
            this.Breaks.Add(tBreak);
            this.SwitchTrendlineType();
        }


        public void AddHit(TrendHit hit)
        {
            this.Hits.Add(hit);
            this.RecalculateScore();
        }


        public void AddBreak(TrendBreak trendBreak){
            this.Breaks.Add(trendBreak);
            this.RecalculateScore();
        }

        public void AddBounce(TrendBounce TrendBounce)
        {
            this.Bounces.Add(TrendBounce);
            this.RecalculateScore();
        }


        public void Finish(DataItem item)
        {
            this.IsFinished = true;
            this.EndDate = item.Date;
            this.RecalculateScore();
        }


        private string calculateHitsMD5()
        {

            var hitsDates = string.Empty;
            foreach (var hit in Hits)
            {
                hitsDates += hit.Item.Date.ToString();
            }


            using (var md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(hitsDates));
                StringBuilder result = new StringBuilder(bytes.Length * 2);

                for (int i = 0; i < bytes.Length; i++)
                {
                    result.Append(bytes[i].ToString("X2"));
                }

                var str = result.ToString();
                return str;

            }


        }


        public TrendHit LastHit()
        {
            if (Hits.Count > 0)
            {
                var maxDate = Hits.Max(h => h.Item.Date);
                return Hits.SingleOrDefault(h => h.Item.Date.CompareTo(maxDate) == 0);
            }
            else
            {
                return null;
            }
        }


        public TrendBounce LastBounce()
        {
            if (Bounces.Count > 0)
            {
                var maxDate = Bounces.Max(b => b.StartHit.Item.Date);
                return Bounces.SingleOrDefault(b => b.StartHit.Item.Date.CompareTo(maxDate) == 0);
            }
            else
            {
                return null;
            }
        }


        public bool IsExtremum(DataItem item)
        {
            return item.IsExtremum(CurrentType);
        }



        /*
         * Funkcja sprawdzająca położenie podanego notowania względem tej
         * linii trendu. Jeżeli notowanie jest położone w odpowiedniej odległości,
         * zostaje ono dodane do kolekcji zdarzeń tej linii trendu (czyli obiektu
         * klasy Trendline). Na podstawie tej kolekcji jest potem oceniana każda
         * linia trendu. */
        public void Check(DataItem item, DataItem nextItem)
        {
            bool isExtremum = IsProperExtremum(item, nextItem);
            double level = GetLevel(item.Index);
            IPriceTrendComparer comparer = isExtremum ? this.extremumComparer : this.otherComparer;
            comparer.Compare(item, level, CurrentType, PreviousHit);

            /* Dodaj wartość wyliczoną dla tego notowania do zmiennej [TotalPriceOverBreak]. */
            this.TotalOverBreak += comparer.GetPriceOverBreak();

            /* Sprawdź czy jest to trafienie. */
            if (comparer.IsHit())
            {
                var hit = comparer.GetHit();
                this.Hits.Add(hit);
                this.PreviousHit = item;
                if (isExtremum) this.PreviousHit = item;
            }


            /* Sprawdź czy jest to przebicie (Cena Close > linia trendu) */
            if (comparer.IsBreak())
            {
                var trendBreak = comparer.GetBreak();

                /* Sprawdź notowania pomiędzy tym przełamaniem, a poprzednim.
                 * Jeżeli nie było w tym czasie żadnego odbicia ekstremum od linii trendu,
                 * linia trendu otrzymuje punkty ujemne - czyli (dla linii oporu):
                 * kurs wzrasta, przebija od dołu linię oporu, nad linią oporu wytrzymuje przez
                 * kilka notowań, ale w tym czasie ani razu nie ma odbicia od tej linii trendu od góry.
                 * Potem wykres wraca z powrotem pod linię oporu, nie zaliczywszzy żadnego odbicia
                 * od linii trendu z góry.
                 * W takiej sytuacji liczone są punkty na podstawie odległości cen zamknięcia
                 * wszystkich notowań, które znalazły się nad linią oporu (i pod linią wsparcia). */
                trendBreak.Score = checkBreakRange(item);

                this.Breaks.Add(trendBreak);
                this.PreviousBreak = item;
                this.TotalOverBreak = 0;
                SwitchTrendlineType();
            }

        }


        private double checkBreakRange(DataItem item)
        {

            int previousIndex = this.PreviousBreak.Index;
            int currentIndex = item.Index;
            int previousHitIndex = this.PreviousHit.Index;

            if (!previousHitIndex.isInRange(previousIndex, currentIndex)){
                return this.TotalOverBreak;
            }

            return 0d;
        }

        private void SwitchTrendlineType()
        {
            if (this.CurrentType == TrendlineType.Resistance)
            {
                this.CurrentType = TrendlineType.Support;
            }
            else
            {
                this.CurrentType = TrendlineType.Resistance;
            }
        }

        public bool IsProperExtremum(DataItem item, DataItem nextItem)
        {


            if (item == null || item.Price == null) return false;


            if (item.Price.IsExtremum(CurrentType))
            {

                if (PreviousHit != null && PreviousHit.Index == item.Index - 1)
                {
                    return false;
                }
                else
                {

                    if (nextItem == null || nextItem.Price == null) return true;

                    return ((CurrentType == TrendlineType.Resistance && nextItem.Price.PeakByHigh == 0) ||
                            (CurrentType == TrendlineType.Support && nextItem.Price.TroughByLow == 0));
                }
            }
            else
            {
                return false;
            }


        }


        //#region CalculatingTrendlineHit

        //private TrendHit createHit(DataItem item)
        //{

        //    /* Create new object of DataItem class. */
        //    TrendHit hit = new TrendHit
        //    {
        //        Item = item,
        //        TrendlineId = this.Trendline.Id,
        //        CrossLevel = calculateCrossLevel(item)
        //    };


        //    hit.DistanceScore = calculateDistance(item, this.trendlineType);

        //    /* If distance criteria is not matched, there is no point to check further. */
        //    if (hit.DistanceScore <= 0) return hit;

        //    //var amplitude = calculateAmplitude(item);
        //    hit.TimeRangeScore = calculateTimeRange(item);
        //    hit.SlopeScore = calculateSlopeScore(this.currentExtremum, item);
        //    hit.EvaluationScore = item.Price.ExtremumEvaluation(this.trendlineType) / 100d;

        //    hit.Calculate();

        //    return hit;

        //    //System.Diagnostics.Debug.WriteLine(item.Date.ToString() + ": " + trendLevels[item.Index] + ";" + distance + ";" + timeRange + ";" + slopeScore + ";" + extremumScore + ";" + total);

        //}


        //public double calculateDistance(DataItem item, TrendlineType type)
        //{

        //    /* Calculate limits. */
        //    double limit = item.Quotation.Close * MaxExtremumVariation;
        //    double level = trendLevels[item.Index];

        //    /* Calculate distance. */
        //    double distance = item.GetClosestDistance(type, level);
        //    double distanceToLimit = 100 * distance / limit;
        //    double score = (1d / Math.Pow(1 + distanceToLimit, 0.1d)) * (1d - (distanceToLimit / 100d));

        //    return item.IsInRange(level) ? Math.Max(score, 0.5d) : Math.Max(score, 0);

        //}


        //public double calculateTimeRange(DataItem item)
        //{

        //    var periods = item.Index - (currentExtremum == null ? 0 : currentExtremum.Index);
        //    return Math.Min((double)periods / (double)MaxRangeWithoutHit, 1d);

        //}


        ///* New version of this method (17.05.2015) covers [slope] and [amplitude] property as well. */
        //public double calculateSlopeScore(DataItem item, DataItem subitem)
        //{


        //    /* If this is the first item, no points are given for slope factor. */
        //    //if (item == null || subitem == null) return 0d;


        //    ///* Check if [Slope] is equal to 0. In such case function always returns 1. */
        //    //if (Slope == 0d) return 1d;



        //    ///* Make sure that trendLevels array is created and filled with data. */
        //    ////if (trendLevels == null) trendLevels = createTrendLevelsArray();


        //    ///* Count distance between all close prices and trend levels in this timerange.*/
        //    //DataItem[] range = Items.Where(i => i.Date > item.Date && i.Date < subitem.Date).ToArray();
        //    //double covered = range.Sum(i => i.GetMinDifference(trendLevels[i.Index]));
        //    //double notCovered = range.Sum(i => Math.Abs(initialLevel - trendLevels[i.Index]));
        //    //double coverage = covered / Math.Pow(notCovered, 2d);
        //    //double result = 1d - (1d / (coverage + 1));

        //    return 0;

        //}


        //public double calculateCrossLevel(DataItem item)
        //{
        //    return trendLevels[item.Index];
        //}

        //#endregion





        ///* Funkcja sprawdza czy dany trend się przeterminował. 
        // * Trend jest przeterminowany, jeżeli przez wystarczająco długi nie było żadnego odbicia od jego linii. */
        //public bool CheckIfTrendIsExpired(int index)
        //{
        //    return false;
        //}






        //public void checkRangeBetweenExtrema(DataItem startItem, DataItem endItem)
        //{

        //}


        //public DataItem[] GetProspectiveBreaks(DataItem startItem, DataItem endItem)
        //{
        //    return null;
        //}


        //public double calculateBreakPoints(DataItem item, DataItem subitem)
        //{


        //    /*
        //     * Potrzebne informacje:
        //     *  - czy została przebita linia trendu (czyli czy zmieniła się wartość TrendType).
        //     *  - czy linia została unieważniona przez mniejsze przebicia.
        //     */

        //    /* If any of items is not defined, return 0. */
        //    if (item == null || subitem == null) return 0d;


        //    TrendlineType initialType = this.trendlineType;
        //    var totalBroke = 0d;
        //    var brokeRow = 0;


        //    /*
        //     * cała świeca poniżej linii trendu && Trend == RESISTANCE --> nic
        //     * cała świeca powyżej linii trendu && Trend == SUPPORT    --> nic
        //     * 
        //     * 
        //     * 
        //     */



        //    for (var i = item.Index + 1; i < subitem.Index; i++)
        //    {

        //        var tick = Items[i];
        //        var trendlevel = trendLevels[i];
        //        var highTrendDifference = subitem.Quotation.High - trendlevel;
        //        var lowTrendDifference = subitem.Quotation.Low - trendlevel;

        //        if (initialType == TrendlineType.Support)
        //        {

        //            if (lowTrendDifference < 0)
        //            {
        //                var difference = highTrendDifference - lowTrendDifference;
        //                totalBroke -= (lowTrendDifference / difference);
        //            }

        //            brokeRow = (brokeRow + 1) * (subitem.Quotation.Close < trendlevel ? 1 : -1);

        //        }
        //        else if (initialType == TrendlineType.Resistance)
        //        {

        //            if (highTrendDifference > 0)
        //            {
        //                var difference = highTrendDifference - lowTrendDifference;
        //                totalBroke += (highTrendDifference / difference);
        //            }

        //            brokeRow = (brokeRow + 1) * (subitem.Quotation.Close > trendlevel ? 1 : -1);

        //        }

        //    }


        //    /*
        //     *  Set the proper value of [isAbove] flag.
        //     */
        //    if (brokeRow > MinForTrendlineBreak)
        //    {
        //        this.trendlineType = (this.trendlineType == TrendlineType.Resistance ? TrendlineType.Support : TrendlineType.Resistance);
        //    }


        //    return (totalBroke / 100) * (this.trendlineType == initialType ? 1 : -1);
        //    //return (totalBroke / totalItems) * (this.trendlineType == initialType ? 1 : -1);

        //}

        //public double calculateSingleCandle(DataItem item, out bool lineBroke)
        //{

        //    TrendlineType type = this.trendlineType;
        //    double limit = (item.Quotation.Close * MaxExtremumVariation) / 2;
        //    double level = trendLevels[item.Index];
        //    double closeDistance = item.GetOpenOrClosePrice(type) - level;
        //    double exDistance = item.GetHighOrLowPrice(type) - level;



        //    /* Assign the proper value to the [lineBroken] flag */
        //    lineBroke = item.IsTrendlineBroken(level, type);


        //    if (closeDistance < -limit && exDistance < -limit)
        //    {
        //        return 0d;
        //    }

        //    if (Math.Abs(exDistance) > limit)
        //    {
        //        //lineBroke = false;
        //        //return 0d;
        //    }
        //    else
        //    {

        //    }

        //    double distance = Math.Min(closeDistance, exDistance);
        //    double distanceToLimit = 100 * distance / limit;
        //    double score = (1d / Math.Pow(1 + distanceToLimit, 0.1d)) * (1d - (distanceToLimit / 100d));

        //    return score;
        //}




    }

}