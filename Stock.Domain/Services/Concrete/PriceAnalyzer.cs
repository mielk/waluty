﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;


namespace Stock.Domain.Services
{
    public class PriceAnalyzer : IPriceAnalyzer
    {
        public const int DirectionCheckCounter = 10;
        public const int DirectionCheckRequired = 6;
        public const int MinRange = 3;
        public const int MaxRange = 180;

        private IDataRepository _dataRepository;
        private IFxRepository _fxRepository;

        public DataItem[] Items { get; set; }
        public bool DebugMode { get; set; }
        public string Symbol;
        public int AssetId;
        public int TimebandId;
        public int StartIndex;
        public int CurrentDirection2D;




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
            Items = _dataRepository.GetFxQuotations(symbol).Select(DataItem.FromDto).ToArray();
            Items.AppendIndexNumbers();
        }

        public void LoadDataSets(DataItem[] items)
        {
            Items = items;
            Items.AppendIndexNumbers();
        }




        public void Analyze(string symbol)
        {

            /* Prepare instance. */
            if (!DebugMode)
            {
                EnsureRepositories();
                LoadParameters(symbol);
                LoadDataSets(symbol);
            };


            DateTime lastDate = FindLastAnalyzed();
            //int index = FindIndex(lastDate);
            int index = 0;
            int start = Math.Max(index - MaxRange, 0);

            for (var i = start; i < Items.Length; i++)
            {
                AnalyzePrice(i);
            }


            /* Start looking for trend lines */
            //ITrendService trendService = new TrendService(Symbol, Items);
            //trendService.Start();

        }

        protected DateTime FindLastAnalyzed()
        {

            var itemsWithPrice = Items.Where(i => i.Price != null).ToArray();

            if (itemsWithPrice.Length == 0)
            {
                return DateTime.MinValue;
            }
            else
            {
                return itemsWithPrice[itemsWithPrice.Length - 1].Date;
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

        protected int FindIndex()
        {
            return FindIndex(FindLastAnalyzed());
        }

        private string GetFxQuotationsTableName(string symbol)
        {
            return "quotations_" + symbol;
        }

        private void AnalyzePrice(int index)
        {

            DataItem item = (index < 0 || index >= Items.Length ? null : Items[index]);

            if (item == null) return;


            //Ensure that [Price] object is appended to this [DataItem].
            if (item.Price == null)
            {
                item.Price = new Price();
                item.Price.Date = item.Date;
            }

            item.Price.CloseDelta = CalculateDeltaClosePrice(item.Quotation.Close, index);
            item.Price.Direction2D = CalculateDirection2D(index);
            item.Price.Direction3D = CalculateDirection3D(index);
            item.Price.PeakByClose = EvaluateExtremum(item, index, true, true);
            item.Price.PeakByHigh = EvaluateExtremum(item, index, true, false);
            item.Price.TroughByClose = EvaluateExtremum(item, index, false, true);
            item.Price.TroughByLow = EvaluateExtremum(item, index, false, false);


            if (item.Price.Id == 0)
            {
                _dataRepository.AddPrice(item.Price.ToDto(), Symbol);
            }
            else
            {
                _dataRepository.UpdatePrice(item.Price.ToDto(), Symbol);
            }


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
            endIndex = left ? Math.Max(index - 360, 0) : Math.Min(index + 360, Items.Length - 1);


            //If the number of items to be checked is less than MinRange, there is no point to check it - 
            //even if all items are better, the function will return 0 anyway.
            if ((Math.Abs(startIndex - endIndex) + 1) < MinRange) return 0;



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

        protected double EvaluateExtremum(DataItem item, int index, bool isPeak, bool byClose)
        {

            var leftSerie = (double)CountSerie(index, isPeak, byClose, true) / (double) MaxRange;
            var rightSerie = (double)CountSerie(index, isPeak, byClose, false) / (double) MaxRange;
            var rangePoints = Math.Sqrt(leftSerie * rightSerie);


            if (rangePoints > 0)
            {
                var volumeFactor = VolumeFactor(item.Quotation.Volume, index);
            }

            return rangePoints * 100;

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



    }

}