using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Enums;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public class ExtremumProcessor : IExtremumProcessor
    {
        private const int DEFAULT_MIN_REQUIRED_MINORS = 3;
        private const int DEFAULT_MAX_SERIE_COUNT = 360;
        public int MinRequiredMinors { get; set; }
        public int MaxSerieCount { get; set; }
        private IProcessManager manager;


        #region CONSTRUCTOR

        public ExtremumProcessor(IProcessManager manager)
        {
            this.manager = manager;
            MinRequiredMinors = DEFAULT_MIN_REQUIRED_MINORS;
            MaxSerieCount = DEFAULT_MAX_SERIE_COUNT;
        }

        #endregion CONSTRUCTOR


        #region INFRASTRUCTURE

        public void InjectProcessManager(IProcessManager manager)
        {
            this.manager = manager;
        }

        private Quotation getQuotation(int index)
        {
            DataSet ds = manager.GetDataSet(index);
            if (ds != null)
            {
                return ds.GetQuotation();
            }
            return null;
        }


        #endregion INFRASTRUCTURE


        public bool IsExtremum(DataSet dataSet, ExtremumType type)
        {
            int index = dataSet.IndexNumber;
            for (int i = index - 1; i >= Math.Max(0, index - MinRequiredMinors); i--)
            {                   
                Quotation comparedQuotation = getQuotation(i);
                bool comparison = compare(dataSet.GetQuotation(), comparedQuotation, type, false);
                if (!comparison) return false;
            }

            for (int i = index + 1; i <= index + MinRequiredMinors; i++)
            {
                Quotation comparedQuotation = getQuotation(i);
                bool comparison = compare(dataSet.GetQuotation(), comparedQuotation, type, true);
                if (!comparison) return false;
            }

            return true;
        }

        private bool compare(Quotation baseQuotation, Quotation comparedQuotation, ExtremumType type, bool isLater)
        {
            
            if (comparedQuotation == null) return false;

            var isPeakIndicator = type.IsPeak() ? 1 : -1;
            var baseValue = (type.ByClose() ? baseQuotation.Close : type == ExtremumType.PeakByHigh ? baseQuotation.High : baseQuotation.Low) * isPeakIndicator;
            var comparedValue = (type.ByClose() ? comparedQuotation.Close : type == ExtremumType.PeakByHigh ? comparedQuotation.High : comparedQuotation.Low) * isPeakIndicator;

            if (isLater)
            {
                return baseValue >= comparedValue;
            }
            else {
                return baseValue > comparedValue;
            }

        }



        public double CalculateEarlierAmplitude(Extremum extremum)
        {
            double maxDifference = 0d;
            int index = extremum.GetIndexNumber();
            int coefficient = (extremum.Type.IsPeak() ? 1 : -1);
            DataSet currentDataSet = manager.GetDataSet(index);
            Quotation quotation = currentDataSet.GetQuotation();
            if (quotation == null) return 0d;

            for (int i = index - 1; i > Math.Max(0, index - MaxSerieCount - 1); i--)
            {
                Quotation comparedQuotation = getQuotation(i);
                if (comparedQuotation != null)
                {
                    if (coefficient * (quotation.GetProperValue(extremum.Type) - comparedQuotation.GetProperValue(extremum.Type)) > 0)
                    {
                        double difference = Math.Abs(quotation.GetProperValue(extremum.Type) - comparedQuotation.GetOppositeValue(extremum.Type));
                        if (difference > maxDifference)
                        {
                            maxDifference = difference;
                        }
                    }
                    else
                    {
                        return maxDifference;
                    }
                }
            }

            return maxDifference;

        }

        public int CalculateEarlierCounter(Extremum extremum)
        {
            int index = extremum.GetIndexNumber();
            int coefficient = (extremum.Type.IsPeak() ? 1 : -1);
            DataSet currentDataSet = manager.GetDataSet(index);
            Quotation quotation = currentDataSet.GetQuotation();
            int counter = 0;
            if (quotation == null) return 0;

            for (int i = index - 1; i > Math.Max(0, index - MaxSerieCount - 1); i--)
            {
                Quotation comparedQuotation = getQuotation(i);
                if (comparedQuotation != null)
                {
                    if (coefficient * (quotation.GetProperValue(extremum.Type) - comparedQuotation.GetProperValue(extremum.Type)) >= 0)
                    {
                        counter++;
                    }
                    else
                    {
                        return counter;
                    }
                }
            }

            return counter;

        }

        public double CalculateEarlierChange(Extremum extremum, int units)
        {
            int index = extremum.GetIndexNumber();
            int coefficient = (extremum.Type.IsPeak() ? 1 : -1);
            DataSet currentDataSet = manager.GetDataSet(index);
            Quotation quotation = currentDataSet.GetQuotation();
            Quotation comparedQuotation = getQuotation(index - units);
            if (comparedQuotation != null)
            {
                double baseValue = quotation.Close; 
                double comparedValue = comparedQuotation.Close;
                var difference = coefficient * (baseValue - comparedValue);
                return (difference / comparedValue);
            }
            else
            {
                return 0d;
            }
        }



        public double CalculateLaterAmplitude(Extremum extremum) {
            double maxDifference = 0d;
            int index = extremum.GetIndexNumber();
            int coefficient = (extremum.Type.IsPeak() ? 1 : -1);
            DataSet currentDataSet = manager.GetDataSet(index);
            Quotation quotation = currentDataSet.GetQuotation();
            if (quotation == null) return 0d;


            bool exit = false;
            while (!exit)
            {
                if (++index > extremum.GetIndexNumber() + MaxSerieCount) break;
                Quotation comparedQuotation = getQuotation(index);
                if (comparedQuotation == null) break;

                if (coefficient * (quotation.GetProperValue(extremum.Type) - comparedQuotation.GetProperValue(extremum.Type)) > 0)
                {
                    double difference = Math.Abs(quotation.GetProperValue(extremum.Type) - comparedQuotation.GetOppositeValue(extremum.Type));
                    if (difference > maxDifference)
                    {
                        maxDifference = difference;
                    }
                }
                else
                {
                    break;
                }

            }

            return maxDifference;
        
        }

        public int CalculateLaterCounter(Extremum extremum)
        {
            int index = extremum.GetIndexNumber();
            int coefficient = (extremum.Type.IsPeak() ? 1 : -1);
            DataSet currentDataSet = manager.GetDataSet(index);
            Quotation quotation = currentDataSet.GetQuotation();
            int counter = 0;
            if (quotation == null) return 0;

            for (int i = index + 1; i <= index + MaxSerieCount; i++)
            {
                Quotation comparedQuotation = getQuotation(i);
                if (comparedQuotation != null)
                {
                    if (coefficient * (quotation.GetProperValue(extremum.Type) - comparedQuotation.GetProperValue(extremum.Type)) >= 0)
                    {
                        counter++;
                    }
                    else
                    {
                        return counter;
                    }
                }
                else
                {
                    return counter;
                }
            }

            return counter;
        
        }

        public double CalculateLaterChange(Extremum extremum, int units)
        {
            int index = extremum.GetIndexNumber();
            int coefficient = (extremum.Type.IsPeak() ? 1 : -1);
            DataSet currentDataSet = manager.GetDataSet(index);
            Quotation quotation = currentDataSet.GetQuotation();
            Quotation comparedQuotation = getQuotation(index + units);
            if (comparedQuotation != null)
            {
                double baseValue = quotation.Close;
                double comparedValue = comparedQuotation.Close;
                var difference = coefficient * (baseValue - comparedValue);
                return (difference / baseValue);
            }
            else
            {
                return 0d;
            }
        }


        public double CalculateValue(Extremum extremum)
        {

            var beforeFactor = ((double)extremum.EarlierCounter / (double)DEFAULT_MAX_SERIE_COUNT);
            var afterFactor = ((double)extremum.LaterCounter / (double)DEFAULT_MAX_SERIE_COUNT);
            var value = Math.Sqrt(beforeFactor * afterFactor) * 200;
            return value;
        }







        public IEnumerable<ExtremumGroup> ExtractExtremumGroups(IEnumerable<DataSet> dataSets)
        {

            List<ExtremumGroup> groups = new List<ExtremumGroup>();
            DataSet[] byDate = dataSets.OrderBy(ds => ds.Date).ToArray();

            for (int i = 1; i < byDate.Length; i++)
            {

                ExtremumGroup peakGroup = getPeakGroup(byDate, i);
                if (peakGroup != null)
                {
                    groups.Add(peakGroup);
                }

                ExtremumGroup troughGroup = getTroughGroup(byDate, i);
                if (troughGroup != null)
                {
                    groups.Add(troughGroup);
                }

            }
            return groups.OrderBy(e => e.GetIndex()).ToArray();
        }

        private ExtremumGroup getPeakGroup(DataSet[] dataSetsByDate, int index)
        {
            DataSet ds = dataSetsByDate[index];
            Extremum peakByClose = getExtremum(dataSetsByDate, index, ExtremumType.PeakByClose);
            if (peakByClose != null)
            {
                if (ds.price.PeakByHigh == null)
                {
                    Extremum previous = getExtremum(dataSetsByDate, index - 1, ExtremumType.PeakByHigh);
                    if (previous != null)
                    {
                        return new ExtremumGroup(peakByClose, previous);
                    }
                    else
                    {
                        Extremum next = getExtremum(dataSetsByDate, index + 1, ExtremumType.PeakByHigh);
                        if (next != null)
                        {
                            return new ExtremumGroup(peakByClose, next);
                        }
                    }
                }
                return new ExtremumGroup(peakByClose, null);
            }
            else
            {
                Extremum peakByHigh = getExtremum(dataSetsByDate, index, ExtremumType.PeakByHigh);
                if (peakByHigh != null)
                {
                    Extremum next = getExtremum(dataSetsByDate, index + 1, ExtremumType.PeakByClose);
                    if (next == null)
                    {
                        Extremum previous = getExtremum(dataSetsByDate, index - 1, ExtremumType.PeakByClose);
                        if (previous == null)
                        {
                            return new ExtremumGroup(null, peakByHigh);
                        }
                    }
                }
            }

            return null;

        }

        private ExtremumGroup getTroughGroup(DataSet[] dataSetsByDate, int index)
        {
            DataSet ds = dataSetsByDate[index];
            Extremum troughByClose = getExtremum(dataSetsByDate, index, ExtremumType.TroughByClose);
            if (troughByClose != null)
            {
                if (ds.price.TroughByLow == null)
                {
                    Extremum previous = getExtremum(dataSetsByDate, index - 1, ExtremumType.TroughByLow);
                    if (previous != null)
                    {
                        return new ExtremumGroup(troughByClose, previous);
                    }
                    else
                    {
                        Extremum next = getExtremum(dataSetsByDate, index + 1, ExtremumType.TroughByLow);
                        if (next != null)
                        {
                            return new ExtremumGroup(troughByClose, next);
                        }
                    }
                }
                return new ExtremumGroup(troughByClose, null);
            }
            else
            {
                Extremum troughByLow = getExtremum(dataSetsByDate, index, ExtremumType.TroughByLow);
                if (troughByLow != null)
                {
                    Extremum next = getExtremum(dataSetsByDate, index + 1, ExtremumType.TroughByClose);
                    if (next == null)
                    {
                        Extremum previous = getExtremum(dataSetsByDate, index - 1, ExtremumType.TroughByClose);
                        if (previous == null)
                        {
                            return new ExtremumGroup(null, troughByLow);
                        }
                    }
                }
            }

            return null;

        }

        private Extremum getExtremum(DataSet[] dataSetsByDate, int index, ExtremumType extremumType)
        {
            if (index >= 0 && index < dataSetsByDate.Length)
            {
                DataSet ds = dataSetsByDate[index];
                Price price = ds.price;
                if (price != null)
                {
                    return price.GetExtremum(extremumType);
                }
            }
            return null;
        }


    }

}