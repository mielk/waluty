﻿using System;
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
            int index = extremum.IndexNumber;
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

    }

}
