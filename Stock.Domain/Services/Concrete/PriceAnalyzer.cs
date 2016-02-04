using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;
using Stock.Domain.Enums;


namespace Stock.Domain.Services
{
    public class PriceAnalyzer : IPriceAnalyzer
    {
        public const AnalysisType Type = AnalysisType.Price;
        public const int DirectionCheckCounter = 10;
        public const int DirectionCheckRequired = 6;
        public const int MinRange = 3;
        public const int MaxRange = 360;

        private IDataRepository _dataRepository;
        private IFxRepository _fxRepository;

        private Analysis analysis;
        public DataItem[] Items { get; set; }
        public bool DebugMode { get; set; }
        public string Symbol;
        public int AssetId;
        public int TimebandId;
        public int CurrentDirection2D;

        //Current peaks & troughs - for better performance.
        private Dictionary<ExtremumType, DataItem> currentExtrema;



        public void Analyze(string symbol)
        {
            Analyze(symbol, false);
        }

        public void Analyze(string symbol, bool fromScratch)
        {


            /* Prepare instance. */
            if (!DebugMode)
            {
                analysis = new Analysis(symbol, Type);
                currentExtrema = new Dictionary<ExtremumType, DataItem>();
                EnsureRepositories();
                LoadParameters(symbol);
            };


            /* Fetch the proper data items and start index.
             * The range of data to be analyzed depends on the [fromScratch] parameter 
             * and the dates of last quotation and last analysis item. 
             * 
             * wideStartIndex - tylko wierzchołki i dołki są przeliczane od nowa (nic innego nie może się zmienić)
             * narrowStartIndex - od tego elementu wszystko przeliczane jest od nowa (bo notowanie też mogło zostać 
             *                      zaktualizowane i mogą się zmienić inne parametry)
             * 
             */
            var wideStartIndex = 0;
            var narrowStartIndex = 0;
            if (fromScratch)
            {
                LoadDataSets(symbol);
            }
            else
            {
                var lastDates = _dataRepository.GetSymbolLastItems(symbol, Type.TableName());

                //Check if analysis is up-to-date. If it is true, leave this method and run it again for the next symbol.
                if (lastDates.IsUpToDate()) return;

                if (lastDates.LastAnalysisItem == null)
                {
                    LoadDataSets(symbol);
                }
                else
                {
                    LoadDataSets(symbol, (DateTime)lastDates.LastAnalysisItem, MaxRange + DirectionCheckCounter);
                    narrowStartIndex = FindIndex((DateTime)lastDates.LastAnalysisItem);
                    wideStartIndex = Math.Max(narrowStartIndex - MaxRange, 0);
                }


            }


            //Save info about this analysis.
            analysis.FirstItemDate = Items[wideStartIndex].Date;
            analysis.LastItemDate = Items[Items.Length - 1].Date;
            analysis.AnalyzedItems = Items.Length - wideStartIndex + 1;

            
            for (var i = wideStartIndex; i < Items.Length; i++)
            {
                AnalyzePrice(i, i >= narrowStartIndex);
            }


            /* Start looking for trend lines */
            //ITrendService trendService = new TrendService(Symbol, Items);
            //trendService.Start();


            //Insert info about this analysis to the database.
            analysis.AnalysisEnd = DateTime.Now;
            _dataRepository.AddAnalysisInfo(analysis.ToDto());


        }




        public void EnsureRepositories()
        {
            //Check if DateRepository is appended.
            if (_dataRepository == null)
            {
                _dataRepository = RepositoryFactory.GetDataRepository();
            }

            //Check if FXRepository is appended.
            if (_fxRepository == null)
            {
                _fxRepository = RepositoryFactory.GetFxRepository();
            }

        }

        public void LoadParameters(string symbol)
        {
            Symbol = symbol;
            var pairSymbol = Symbol.Substring(0, Symbol.IndexOf('_'));
            var pair = _fxRepository.GetPair(pairSymbol);
            AssetId = pair.Id;
        }

        public void LoadDataSets(string symbol)
        {
            Items = _dataRepository.GetFxQuotationsForAnalysis(symbol, Type.TableName()).Select(DataItem.FromDto).ToArray();
            Items.AppendIndexNumbers();
        }

        public void LoadDataSets(DataItem[] items)
        {
            Items = items;
            Items.AppendIndexNumbers();
        }

        public void LoadDataSets(string symbol, DateTime lastAnalysisItem, int counter)
        {
            Items = _dataRepository.GetFxQuotationsForAnalysis(symbol, Type.TableName(), lastAnalysisItem, counter).Select(DataItem.FromDto).ToArray();
            Items.AppendIndexNumbers();
            LoadExtrema();
        }

        public void LoadExtrema()
        {

            var firstQuotationDate = Items.Min(q => q.Date);
            var lastQuotationDate = Items.Max(q => q.Date);
            var extrema = _dataRepository.GetExtrema(Symbol, firstQuotationDate, lastQuotationDate);

            foreach (var extremumDto in extrema)
            {
                var item = Items.SingleOrDefault(q => q.Date.Equals(extremumDto.PriceDate));
                if (item != null && item.Price != null)
                {
                    item.Price.ApplyExtremumValue(Extremum.FromDto(extremumDto));
                }
            }

        }


        protected int FindIndex(DateTime date)
        {

            //If there is no item, return 0.
            if (Items.Length == 0) return 0;


            //If the given date is earlier than first item, return 0.
            if (date.CompareTo(Items[0].Date) <= 0) return 0;


            //If the given date is later than the last item, return the last item's index.
            if (date.CompareTo(Items[Items.Length - 1].Date) >= 0) return Items.Length - 1;


            //Otherwise, iterate through items and find index.
            for (var i = 0; i < Items.Length; i++)
            {

                int comparison = date.CompareTo(Items[i].Date);

                if (comparison == 0)
                {
                    return i;
                }
                else if (comparison < 0)
                {
                    return Math.Max(i - 1, 0);
                }
            }


            return 0;

        }

        private string GetFxQuotationsTableName(string symbol)
        {
            return "quotations_" + symbol;
        }

        private void AnalyzePrice(int index, bool fromScratch)
        {

            //Check if this item should be analyzed.
            DataItem item = (index < 0 || index >= Items.Length ? null : Items[index]);
            if (item == null) return;
            if (item.Quotation == null) return;
            //If this item nor any later item has complete quotation, it means the previous one
            //was the last real quotation and analysis should finish here.
            if (!item.Quotation.IsComplete() && !LaterQuotationsExists(index)) return;


            //Check if quotation is missing (but only in the middle of not-missing quotations, 
            //because missing quotations at the end of array was excluded one line above).
            //If it is copy data from the previous quotation.
            if (!item.Quotation.IsComplete())
            {
                var previousQuotation = (index > 0 ? Items[index - 1].Quotation : null);
                if (previousQuotation != null)
                {
                    item.Quotation.CompleteMissing(previousQuotation);
                    _dataRepository.UpdateQuotation(item.Quotation.ToDto(), Symbol);
                }
                
            }


            //Ensure that [Price] object is appended to this [DataItem].
            var isChanged = false;
            if (item.Price == null || fromScratch)
            {
                item.Price = new Price();
                item.Price.Date = item.Date;
                item.Price.CloseDelta = CalculateDeltaClosePrice(item.Quotation.Close, index);
                item.Price.Direction2D = CalculateDirection2D(index);
                item.Price.Direction3D = CalculateDirection3D(index);
            }


            //Calculate new values for peaks and troughs and apply them to the current item price.
            //If any of this is changed, this price will have flag [IsChange] set to @true.
            //This is the only thing that can be changed for items being only updated.
            if (CheckForExtremum(item, ExtremumType.PeakByClose, fromScratch)) isChanged = true;
            if (CheckForExtremum(item, ExtremumType.PeakByHigh, fromScratch)) isChanged = true;
            if (CheckForExtremum(item, ExtremumType.TroughByClose, fromScratch)) isChanged = true;
            if (CheckForExtremum(item, ExtremumType.TroughByLow, fromScratch)) isChanged = true;



            if (item.Price.Id == 0)
            {
                _dataRepository.AddPrice(item.Price.ToDto(), Symbol);
            }
            else if (isChanged)
            {
                _dataRepository.UpdatePrice(item.Price.ToDto(), Symbol);
            }

        }

        private bool LaterQuotationsExists(int index)
        {
            for (var i = index; i < Items.Length; i++)
            {
                if (Items[i].Quotation.IsComplete()) return true;
            }
            return false;
        }



        private double CalculateDeltaClosePrice(double close, int index)
        {

            return index == 0 ? 0d : (close - Items[index - 1].Quotation.Close);

        }

        private int CalculateDirection2D(int index)
        {

            var start = Math.Max(index - DirectionCheckCounter, 1);
            var plus = 0;
            var minus = 0;
            
            for (var i = start; i <= index; i++){

                var value = Items[i].Quotation.Close;
                var prevValue = Items[i - 1].Quotation.Close;


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

            return (index > 0 ? Items[index - 1].Price.Direction2D : 0);

        }

        private int CalculateDirection3D(int index)
        {

            var start = Math.Max(index - DirectionCheckCounter, 1);
            var plus = 0;
            var minus = 0;

            for (var i = start; i <= index; i++)
            {

                var value = Items[i].Quotation.Close;
                var prevValue = Items[i - 1].Quotation.Close;


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
            var value = Items[index].GetValue(isPeak, byClose);

            //Calculate startIndex and endIndex.
            startIndex = index + (left ? -1 : 1);
            endIndex = left ? Math.Max(index - MaxRange, 0) : Math.Min(index + MaxRange, Items.Length - 1);


            //If the number of items to be checked is less than MinRange, there is no point to check it - 
            //even if all items are better, the function will return 0 anyway.
            //if ((Math.Abs(startIndex - endIndex) + 1) < MinRange) return 0;
            if (startIndex < 0 || startIndex >= Items.Length || endIndex < 0 || endIndex >= Items.Length) return 0;


            //var i = startIndex;
            int step = (startIndex > endIndex ? -1 : 1);


            for (var i = startIndex; Math.Abs(startIndex - i) <= Math.Abs(startIndex - endIndex); i += step)
            {

                var item = Items[i];
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

        protected double FindPriceAmplitude(DataItem item, Extremum extremum, bool earlier)
        {

            var prevExtremum = getCurrentExtremum(extremum.Type);
            DateTime startDate;
            DateTime endDate;

            if (earlier)
            {
                startDate = Items[Math.Max(0, item.Index - extremum.EarlierCounter)].Date;
                endDate = Items[Math.Max(0, item.Index - 1)].Date;
            }
            else
            {
                startDate = Items[Math.Min(item.Index + 1, Items.Length - 1)].Date;
                endDate = Items[Math.Min(item.Index + extremum.LaterCounter, Items.Length - 1)].Date;
            }
            var itemsRange = Items.Where(i => i.Date >= startDate && i.Date <= endDate);
            double oppositeValue = (extremum.Type.IsPeak() ? 
                                        itemsRange.Min(i => i.Quotation.Low) : 
                                        itemsRange.Max(i => i.Quotation.High));

            return Math.Abs(oppositeValue - item.Quotation.ProperValue(extremum.Type));

        }

        protected double GetPriceChange(DataItem item, Extremum extremum, bool earlier, int counter)
        {

            var index = earlier ?
                Math.Max(0, item.Index - counter) :
                Math.Min(item.Index + counter, Items.Length - 1);

            var value = Items[index].Quotation.ProperValue(extremum.Type);
            var difference = item.Quotation.ProperValue(extremum.Type) - value;
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
                if (laterCounter < MinRange && laterCounter < (Items.Length - 1 - item.Index))
                {
                    extremum.Cancelled = true;
                    return true;
                }
                else
                {
                    extremum.LaterCounter = laterCounter;
                }
                

            } else {
                //Wartości oparte na wcześniejszych notowaniach obliczane są tylko, jeżeli analiza wykonywana jest od zera.
                var earlierCounter = CountSerie(item.Index, type.IsPeak(), type.ByClose(), true);
                var laterCounter = CountSerie(item.Index, type.IsPeak(), type.ByClose(), false);

                //Jeżeli liczba wcześniejszych lub późniejszych notowań gorszych od tego notowania nie osiągnęła 
                //minimalnego poziomu, to notowanie jest dyskwalifikowane jako ekstremum i nie ma sensu go dalej sprawdzać.
                if (earlierCounter < MinRange) return false;
                if (laterCounter < MinRange && laterCounter < (Items.Length - 1 - item.Index)) return false;

                extremum = new Extremum(item.Date, Symbol, type.IsPeak(), type.ByClose());
                extremum.EarlierCounter = earlierCounter;
                extremum.LaterCounter = laterCounter;
                extremum.EarlierAmplitude = FindPriceAmplitude(item, extremum, true);
                extremum.EarlierChange1 = GetPriceChange(item, extremum, true, 1);
                extremum.EarlierChange2 = GetPriceChange(item, extremum, true, 2);
                extremum.EarlierChange3 = GetPriceChange(item, extremum, true, 3);
                extremum.EarlierChange5 = GetPriceChange(item, extremum, true, 5);
                extremum.EarlierChange10 = GetPriceChange(item, extremum, true, 10);
                extremum.Volatility = item.Quotation.Volatility();

            }


            //Właściwie, to już wcześniej zostało zapewnione, że do tego miejsca wykonanie programu dotrze
            //tylko, jeżeli extremum nie jest puste, ale mimo to kompilator nie przepuszcza bez takiego warunku tutaj.
            if (extremum != null)
            {
                extremum.LaterAmplitude = FindPriceAmplitude(item, extremum, false);
                extremum.LaterChange1 = GetPriceChange(item, extremum, false, 1);
                extremum.LaterChange2 = GetPriceChange(item, extremum, false, 2);
                extremum.LaterChange3 = GetPriceChange(item, extremum, false, 3);
                extremum.LaterChange5 = GetPriceChange(item, extremum, false, 5);
                extremum.LaterChange10 = GetPriceChange(item, extremum, false, 10);
                extremum.IsOpen = (item.Index + extremum.LaterCounter == Items.Length - 1);
                setCurrentExtremum(type, item);
                item.Price.ApplyExtremumValue(type, extremum);
            }

            return true;

        }


        protected double VolumeFactor(double volume, int index)
        {

            const int SideRange = 10;
            var start = Math.Max(index - SideRange, 0);
            var end = Math.Min(index + SideRange, Items.Length - 1);
            var counter = 0;
            var sum = 0;
            


            for (var i = start; i <= end; i++)
            {
                counter++;
                sum += (int)Items[i].Quotation.Volume;
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
            currentExtrema[type] = item;
        }

    }

}
