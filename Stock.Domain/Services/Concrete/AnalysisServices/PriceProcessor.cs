using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class PriceProcessor : IPriceProcessor
    {

        private IPriceAnalyzer analyzer;
        private Dictionary<ExtremumType, DataItem> currentExtrema = new Dictionary<ExtremumType, DataItem>();
        public const int DirectionCheckCounter = 10;
        public const int DirectionCheckRequired = 6;
        public const int MinRange = 3;
        public const int MaxRange = 360;
        public int CurrentDirection2D;
        private AssetTimeframe atf;


        public PriceProcessor(IPriceAnalyzer analyzer)
        {
            this.analyzer = analyzer;
        }

        public void reset()
        {
            currentExtrema = new Dictionary<ExtremumType, DataItem>();
        }

        public void runRightSide(IAnalyzer analyzer, int index, AssetTimeframe atf) 
        {
            DataItem item = analyzer.getDataItem(index);
            runRightSide(analyzer, item, atf);
        }

        public void runRightSide(IAnalyzer analyzer, DataItem item, AssetTimeframe atf)
        {
            this.atf = atf;
            if (item == null) return;
            if (item.Quotation == null) return;


            //Calculate new values for peaks and troughs and apply them to the current item price.
            //If any of this is changed, this price will have flag [IsChange] set to @true.
            //This is the only thing that can be changed for items being only updated.
            CheckForExtremum(item, ExtremumType.PeakByClose, false);
            CheckForExtremum(item, ExtremumType.PeakByHigh, false);
            CheckForExtremum(item, ExtremumType.TroughByClose, false);
            CheckForExtremum(item, ExtremumType.TroughByLow, false);
            CheckPriceGap(item);


        }

        private void runLeftSide(IAnalyzer analyzer, DataItem item, AssetTimeframe atf)
        {
            //Check if quotation is missing (but only in the middle of not-missing quotations, 
            //because missing quotations at the end of array was excluded one line above).
            //If it is copy data from the previous quotation.
            if (!item.Quotation.IsComplete())
            {
                var previousQuotation = (item.Index > 0 ? analyzer.getDataItem(item.Index - 1).Quotation : null);
                if (previousQuotation != null)
                {
                    item.Quotation.CompleteMissing(previousQuotation);
                    //_dataService.UpdateQuotation(item.Quotation, Symbol);
                }

            }


            //Ensure that [Price] object is appended to this [DataItem].
            item.Price = new Price();
            item.Price.Date = item.Date;
            if (item.Index > 0) item.Price.CloseDelta = CalculateDeltaClosePrice(item.Quotation.Close, item.Index);
            item.Price.Direction2D = CalculateDirection2D(item.Index);
            item.Price.Direction3D = CalculateDirection3D(item.Index);

            //Calculate new values for peaks and troughs and apply them to the current item price.
            //If any of this is changed, this price will have flag [IsChange] set to @true.
            //This is the only thing that can be changed for items being only updated.
            CheckForExtremum(item, ExtremumType.PeakByClose, true);
            CheckForExtremum(item, ExtremumType.PeakByHigh, true);
            CheckForExtremum(item, ExtremumType.TroughByClose, true);
            CheckForExtremum(item, ExtremumType.TroughByLow, true);
            CheckPriceGap(item);

        }

        public void runFull(IAnalyzer analyzer, int index, AssetTimeframe atf)
        {
            DataItem item = analyzer.getDataItem(index);
            runFull(analyzer, item, atf);
        }

        public void runFull(IAnalyzer analyzer, DataItem item, AssetTimeframe atf)
        {
            this.atf = atf;
            if (item == null) return;
            if (item.Quotation == null) return;

            runLeftSide(analyzer, item, atf);

        }



        private void SaveChanges()
        {

            for (var i = 0; i < analyzer.getDataItemsLength(); i++)
            {
                var item = analyzer.getDataItem(i);

                if (item.Price.Id == 0)
                {
                    //_dataService.AddPrice(item.Price, Symbol);
                }
                else if (item.Price.IsUpdated)
                {
                    //_dataService.UpdatePrice(item.Price, Symbol);
                }

            }

        }




        //private bool LaterQuotationsExists(int index)
        //{
        //    for (var i = index; i < analyzer.getDataItemsLength(); i++)
        //    {
        //        DataItem item = analyzer.getDataItem(i);
        //        if (item.Price.IsComplete) return true;
        //    }
        //    return false;
        //}


        private void CheckPriceGap(DataItem item)
        {

            //If [PriceGap] is already calculated, no need to do it again.
            if (item.Price.PriceGap != 0) return;

            //It is impossible to calculate price gap for the first and last item.
            if (item.Index == 0) return;
            if (item.Index == analyzer.getDataItemsLength() - 1) return;

            var previousItem = analyzer.getDataItem(item.Index - 1);
            var nextItem = analyzer.getDataItem(item.Index + 1);

            if (previousItem.Quotation.High < nextItem.Quotation.Low)
            {
                item.Price.PriceGap = 100 * ((nextItem.Quotation.Low - previousItem.Quotation.High) / item.Quotation.Close);
                item.Price.IsUpdated = true;
            }
            else if (previousItem.Quotation.Low > nextItem.Quotation.High)
            {
                item.Price.PriceGap = 100 * ((nextItem.Quotation.High - previousItem.Quotation.Low) / item.Quotation.Close);
                item.Price.IsUpdated = true;
            }

        }

        private double CalculateDeltaClosePrice(double close, int index)
        {
            DataItem previousItem = analyzer.getDataItem(index - 1);
            return index == 0 ? 0d : (close -  previousItem.Quotation.Close);

        }

        private int CalculateDirection2D(int index)
        {

            var start = Math.Max(index - DirectionCheckCounter, 1);
            var plus = 0;
            var minus = 0;

            for (var i = start; i <= index; i++)
            {
                DataItem activeItem = analyzer.getDataItem(index);
                DataItem previousItem = analyzer.getDataItem(index - 1);
                var value = activeItem.Quotation.Close;
                var prevValue = previousItem.Quotation.Close;


                if (value > prevValue)
                {
                    plus++;
                    if (plus >= DirectionCheckRequired) return 1;
                }
                else if (value < prevValue)
                {
                    minus++;
                    if (minus >= DirectionCheckRequired) return -1;
                }

            }

            return (index > 0 ? analyzer.getDataItem(index - 1).Price.Direction2D : 0);

        }

        private int CalculateDirection3D(int index)
        {

            var start = Math.Max(index - DirectionCheckCounter, 1);
            var plus = 0;
            var minus = 0;

            for (var i = start; i <= index; i++)
            {
                 
                var value = analyzer.getDataItem(index).Quotation.Close;
                var prevValue = analyzer.getDataItem(index - 1).Quotation.Close;


                if (value > prevValue)
                {
                    plus++;
                    if (plus >= DirectionCheckRequired) return 1;
                }
                else if (value < prevValue)
                {
                    minus++;
                    if (minus >= DirectionCheckRequired) return -1;
                }

            }

            return 0;

        }


        protected int CountSerie(int index, bool isPeak, bool byClose, bool left)
        {

            var startIndex = 0;
            var endIndex = 0;
            var counter = 0;
            var value = analyzer.getDataItem(index).GetValue(isPeak, byClose);

            //Calculate startIndex and endIndex.
            startIndex = index + (left ? -1 : 1);
            endIndex = left ? Math.Max(index - MaxRange, 0) : Math.Min(index + MaxRange, analyzer.getDataItemsLength() - 1);


            //If the number of items to be checked is less than MinRange, there is no point to check it - 
            //even if all items are better, the function will return 0 anyway.
            //if ((Math.Abs(startIndex - endIndex) + 1) < MinRange) return 0;
            if (startIndex < 0 || startIndex >= analyzer.getDataItemsLength() || endIndex < 0 || endIndex >= analyzer.getDataItemsLength()) return 0;


            //var i = startIndex;
            int step = (startIndex > endIndex ? -1 : 1);


            for (var i = startIndex; Math.Abs(startIndex - i) <= Math.Abs(startIndex - endIndex); i += step)
            {

                var item = analyzer.getDataItem(i);
                var comparedValue = item.GetValue(isPeak, byClose);
                var broken = (isPeak ?
                                    (left ? (comparedValue > value) : (comparedValue >= value)) :
                                    (left ? (comparedValue < value) : (comparedValue <= value))
                             );


                //Check if serie is broken in this quotation.
                if (broken)
                {
                    return (counter < MinRange ? 0 : counter);
                }


                //Add next point to the result counter.
                counter++;


                //If counter already exceed MaxRange, finish counting - MaxRange will be returned anyway.
                if (counter == MaxRange) return MaxRange;

            }


            return Math.Min(counter, MaxRange);


        }


        protected double FindLaterPriceAmplitude(ExtremumType type, DataItem item, DataItem nextExtremum)
        {
            int startIndex = item.Index + 1;
            int endIndex = nextExtremum.Index - 1;
            var itemsRange = analyzer.getDataItems().Where(i => i.Index >= startIndex && i.Index <= endIndex);

            if (itemsRange.Count() == 0) return 0;

            double oppositeValue = (type.IsPeak() ?
                                        itemsRange.Min(i => i.Quotation.Low) :
                                        itemsRange.Max(i => i.Quotation.High));
            double baseValue = item.Quotation.ProperValue(type);

            return Math.Abs(oppositeValue - baseValue) / Math.Max(oppositeValue, baseValue);

        }

        protected double FindEarlierPriceAmplitude(ExtremumType type, DataItem item, DataItem prevExtremum)
        {

            int startIndex;
            int endIndex;

            startIndex = (prevExtremum == null ? 0 : prevExtremum.Index);
            endIndex = item.Index - 1;
            IEnumerable<DataItem> items = analyzer.getDataItems();
            var itemsRange = items.Where(i => i.Index >= startIndex && i.Index <= endIndex);
            if (itemsRange.Count() == 0) return 0;
            double oppositeValue = (type.IsPeak() ?
                                        itemsRange.Min(i => i.Quotation.Low) :
                                        itemsRange.Max(i => i.Quotation.High));
            double baseValue = item.Quotation.ProperValue(type);

            return Math.Abs(oppositeValue - baseValue) / Math.Max(oppositeValue, baseValue);

        }

        protected double? GetPriceChange(DataItem item, Extremum extremum, bool earlier, int counter)
        {

            if (!earlier && quotationsLeft(item) < counter) return null;

            var index = earlier ?
                Math.Max(0, item.Index - counter) :
                Math.Min(item.Index + counter, analyzer.getDataItemsLength() - 1);

            double comparedValue = analyzer.getDataItem(index).Quotation.ProperValue(extremum.Type);
            double baseValue = item.Quotation.ProperValue(extremum.Type);
            double difference = (baseValue - comparedValue) / Math.Max(comparedValue, baseValue);
            return difference * (extremum.Type.IsPeak() ? 1 : -1);
        }

        protected bool CheckForExtremum(DataItem item, ExtremumType type, bool fromScratch)
        {

            Extremum extremum;

            if (!fromScratch)
            {
                //Jeżeli analiza nie jest przeprowadzana od początku, sprawdzane jest czy dla tego DataItemu
                //przypisany jest obieket ExtremumCalculator danego typu. Jeżeli nie, oznacza to, że już
                //wcześniej został zdyskwalifikowany i nie ma sensu go sprawdzać.
                extremum = item.Price.GetExtremumObject(type);
                if (extremum == null) return false;
                if (!extremum.IsOpen) return false;

                //Sprawdź czy notowania późniejsze względem tego pozwalają uznać je za ekstremum.
                var laterCounter = CountSerie(item.Index, type.IsPeak(), type.ByClose(), false);
                if (laterCounter < MinRange && laterCounter < (analyzer.getDataItemsLength() - 1 - item.Index))
                {
                    extremum.Cancelled = true;
                    item.Price.ApplyExtremumValue(type, null);
                    item.Price.IsUpdated = true;
                    return true;
                }
                else
                {
                    extremum.LaterCounter = laterCounter;
                }


            }
            else
            {
                //Wartości oparte na wcześniejszych notowaniach obliczane są tylko, jeżeli analiza wykonywana jest od zera.
                var earlierCounter = CountSerie(item.Index, type.IsPeak(), type.ByClose(), true);
                var laterCounter = CountSerie(item.Index, type.IsPeak(), type.ByClose(), false);

                //Jeżeli liczba wcześniejszych lub późniejszych notowań gorszych od tego notowania nie osiągnęła 
                //minimalnego poziomu, to notowanie jest dyskwalifikowane jako ekstremum i nie ma sensu go dalej sprawdzać.
                if (earlierCounter < MinRange) return false;
                if (laterCounter < MinRange && laterCounter < (analyzer.getDataItemsLength() - 1 - item.Index)) return false;

                extremum = new Extremum(item.Date, atf, type.IsPeak(), type.ByClose());
                extremum.EarlierCounter = earlierCounter;
                extremum.LaterCounter = laterCounter;
                extremum.EarlierAmplitude = FindEarlierPriceAmplitude(type, item, getCurrentExtremum(type));
                extremum.EarlierChange1 = GetPriceChange(item, extremum, true, 1);
                extremum.EarlierChange2 = GetPriceChange(item, extremum, true, 2);
                extremum.EarlierChange3 = GetPriceChange(item, extremum, true, 3);
                extremum.EarlierChange5 = GetPriceChange(item, extremum, true, 5);
                extremum.EarlierChange10 = GetPriceChange(item, extremum, true, 10);
                extremum.Volatility = item.Quotation.Volatility();

                //Calculate [LaterAmplitude] for previous extremum.
                var prevExtremumDataItem = getCurrentExtremum(type);
                if (prevExtremumDataItem != null)
                {
                    var prevExtremum = prevExtremumDataItem.Extremum(type);
                    if (prevExtremum != null)
                    {
                        var laterAmplitude = FindLaterPriceAmplitude(type, prevExtremumDataItem, item);
                        prevExtremum.LaterAmplitude = laterAmplitude;
                        prevExtremumDataItem.Price.IsUpdated = true;
                    }
                }

            }


            //Właściwie, to już wcześniej zostało zapewnione, że do tego miejsca wykonanie programu dotrze
            //tylko, jeżeli extremum nie jest puste, ale mimo to kompilator nie przepuszcza bez takiego warunku tutaj.
            if (extremum != null)
            {
                //extremum.LaterAmplitude = FindPriceAmplitude(item, extremum, false);
                if (extremum.LaterChange1 == null) extremum.LaterChange1 = GetPriceChange(item, extremum, false, 1);
                if (extremum.LaterChange2 == null) extremum.LaterChange2 = GetPriceChange(item, extremum, false, 2);
                if (extremum.LaterChange3 == null) extremum.LaterChange3 = GetPriceChange(item, extremum, false, 3);
                if (extremum.LaterChange5 == null) extremum.LaterChange5 = GetPriceChange(item, extremum, false, 5);
                if (extremum.LaterChange10 == null) extremum.LaterChange10 = GetPriceChange(item, extremum, false, 10);
                extremum.IsOpen = (item.Index + extremum.LaterCounter == analyzer.getDataItemsLength() - 1) || quotationsLeft(item) < 10;
                if (extremum.IsConfirmed())
                {
                    setCurrentExtremum(type, item);
                }
                item.Price.ApplyExtremumValue(type, extremum);
                item.Price.IsUpdated = true;
            }

            return true;

        }


        /*
         * Funkcja zwraca liczbę notowań, które zostały do końca zestawu.
         */
        private int quotationsLeft(DataItem item)
        {
            return analyzer.getDataItemsLength() - item.Index - 1;
        }

        protected double VolumeFactor(double volume, int index)
        {

            const int SideRange = 10;
            var start = Math.Max(index - SideRange, 0);
            var end = Math.Min(index + SideRange, analyzer.getDataItemsLength() - 1);
            var counter = 0;
            var sum = 0;



            for (var i = start; i <= end; i++)
            {
                counter++;
                sum += (int)(analyzer.getDataItem(i).Quotation.Volume);
            }


            var avgVolume = (counter == 0 ? 0d : (double)sum / (double)counter);


            return (avgVolume == 0 ? 1d : (double)volume / (double)avgVolume);


        }

        private DataItem getCurrentExtremum(ExtremumType type)
        {
            DataItem item;
            if (currentExtrema.TryGetValue(type, out item))
            {
                return item;
            }
            return null;
        }

        private void setCurrentExtremum(ExtremumType type, DataItem item)
        {

            DataItem previous = null;
            try
            {
                currentExtrema.TryGetValue(type.GetOppositeByPriceLevel(), out previous);
            }
            catch (Exception)
            {
            }

            if (previous != null){
                if (previous.Index != item.Index){
                    if (Math.Abs(previous.Index - item.Index) == 1)
                    {
                        if (type.IsByClosePrice())
                        {
                            item.SlaveExtremum = previous;
                            previous.MasterExtremum = item;
                        }
                        else
                        {
                            item.MasterExtremum = previous;
                            previous.SlaveExtremum = item;
                        }
                    }
                }
            }

            currentExtrema[type] = item;
        }


    }
}
