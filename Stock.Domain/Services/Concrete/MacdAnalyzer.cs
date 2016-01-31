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
    public class MacdAnalyzer : IMacdAnalyzer
    {
        public const AnalysisType Type = AnalysisType.MACD;
        //Calculation params.
        private const int Fast = 13;
        private const int Slow = 26;
        private const int MacdPeriod = 9;

        private IDataRepository _dataRepository;
        private IFxRepository _fxRepository;

        public DataItem[] Items { get; set; }
        public bool DebugMode { get; set; }
        public string Symbol;
        public int AssetId;
        public int TimebandId;
        



        public void Analyze(string symbol)
        {
            Analyze(symbol, false);
        }

        public void Analyze(string symbol, bool fromScratch)
        {

            /* Prepare instance. */
            if (!DebugMode)
            {
                EnsureRepositories();
                LoadParameters(symbol);
            };


            /* Fetch the proper data items and start index.
             * The range of data to be analyzed depends on the [fromScratch] parameter 
             * and the dates of last quotation and last analysis item. 
             */
            var startIndex = 0;
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
                    LoadDataSets(symbol, (DateTime)lastDates.LastAnalysisItem, 10);
                    startIndex = FindIndex((DateTime)lastDates.LastAnalysisItem);
                }

            }


            for (var i = startIndex; i < Items.Length; i++)
            {
                AnalyzePrice(i, fromScratch);
            }


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
            if (item.Macd == null || fromScratch)
            {
                item.Macd = new Macd();
                item.Macd.Date = item.Date;
                item.Macd.Ma13 = calculateAverage(index, Fast);


                item.Macd.Ma26 = calculateAverage(index, Slow);


            }





            if (item.Macd.Id == 0)
            {
                _dataRepository.AddMacd(item.Macd.ToDto(), Symbol);
            }
            else if (isChanged)
            {
                _dataRepository.UpdateMacd(item.Macd.ToDto(), Symbol);
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

        private double calculateAverage(int index, int counter)
        {
            int startIndex = Math.Max(index - counter + 1, 0);
            double sum = 0d;
            for (var i = startIndex; i <= index; i++)
            {
                sum += Items[i].Quotation.Close;
            }

            return sum / (index - startIndex + 1);

        }

    }

}
