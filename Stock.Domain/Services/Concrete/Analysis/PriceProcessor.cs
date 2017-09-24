using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock.Domain.Services
{
    public class PriceProcessor : IPriceProcessor
    {
        private ExtremumType[] extremumTypes = new ExtremumType[] { ExtremumType.PeakByClose, ExtremumType.PeakByHigh, ExtremumType.TroughByClose, ExtremumType.TroughByLow };
        private IProcessManager manager;
        private IExtremumProcessor extremumProcessor;


        #region CONSTRUCTOR

        public PriceProcessor(IProcessManager manager)
        {
            this.manager = manager;
        }

        #endregion CONSTRUCTOR


        #region INFRASTRUCTURE

        public void InjectExtremumProcessor(IExtremumProcessor processor)
        {
            this.extremumProcessor = processor;
            processor.InjectProcessManager(manager);
        }

        private IExtremumProcessor getExtremumProcessor()
        {
            if (extremumProcessor == null)
            {
                extremumProcessor = new ExtremumProcessor(manager);
            }
            return extremumProcessor;
        }

        #endregion INFRASTRUCTURE

        

        public void Process(DataSet dataSet)
        {
            if (dataSet != null && dataSet.GetQuotation() != null)
            {
                createPriceObjectIfNotExist(dataSet);
                calculateDelta(dataSet);
                processExtrema(dataSet, extremumTypes);
            }
        }

        private void createPriceObjectIfNotExist(DataSet dataSet)
        {
            if (dataSet != null && dataSet.GetPrice() == null)
            {
                Price price = new Price()
                {
                    AssetId = dataSet.AssetId,
                    TimeframeId = dataSet.TimeframeId,
                    Date = dataSet.Date,
                    IndexNumber = dataSet.IndexNumber,
                    IsNew = true,
                    SimulationId = manager.GetSimulationId()
                };
                dataSet.SetPrice(price);
            }
        }

        private void calculateDelta(DataSet dataSet)
        {
            DataSet previousDataSet = manager.GetDataSet(dataSet.IndexNumber - 1);
            if (previousDataSet != null && previousDataSet.GetQuotation() != null)
            {
                dataSet.GetPrice().CloseDelta = (dataSet.GetQuotation().Close - previousDataSet.GetQuotation().Close);
            }
        }




        //private void CheckPriceGap(DataItem item)
        //{

        //    //If [PriceGap] is already calculated, no need to do it again.
        //    if (item.Price.PriceGap != 0) return;

        //    //It is impossible to calculate price gap for the first and last item.
        //    if (item.Index == 0) return;
        //    if (item.Index == analyzer.getDataItemsLength() - 1) return;

        //    var previousItem = analyzer.getDataItem(item.Index - 1);
        //    var nextItem = analyzer.getDataItem(item.Index + 1);

        //    if (previousItem.Quotation.High < nextItem.Quotation.Low)
        //    {
        //        item.Price.PriceGap = 100 * ((nextItem.Quotation.Low - previousItem.Quotation.High) / item.Quotation.Close);
        //        item.Price.IsUpdated = true;
        //    }
        //    else if (previousItem.Quotation.Low > nextItem.Quotation.High)
        //    {
        //        item.Price.PriceGap = 100 * ((nextItem.Quotation.High - previousItem.Quotation.Low) / item.Quotation.Close);
        //        item.Price.IsUpdated = true;
        //    }

        //}

        //private int CalculateDirection3D(int index)
        //{

        //    var start = Math.Max(index - DirectionCheckCounter, 1);
        //    var plus = 0;
        //    var minus = 0;

        //    for (var i = start; i <= index; i++)
        //    {

        //        var value = analyzer.getDataItem(index).Quotation.Close;
        //        var prevValue = analyzer.getDataItem(index - 1).Quotation.Close;


        //        if (value > prevValue)
        //        {
        //            plus++;
        //            if (plus >= DirectionCheckRequired) return 1;
        //        }
        //        else if (value < prevValue)
        //        {
        //            minus++;
        //            if (minus >= DirectionCheckRequired) return -1;
        //        }

        //    }

        //    return 0;

        //}

        //private int CalculateDirection2D(int index)
        //{

        //    var start = Math.Max(index - DirectionCheckCounter, 1);
        //    var plus = 0;
        //    var minus = 0;

        //    for (var i = start; i <= index; i++)
        //    {
        //        DataItem activeItem = analyzer.getDataItem(index);
        //        DataItem previousItem = analyzer.getDataItem(index - 1);
        //        var value = activeItem.Quotation.Close;
        //        var prevValue = previousItem.Quotation.Close;


        //        if (value > prevValue)
        //        {
        //            plus++;
        //            if (plus >= DirectionCheckRequired) return 1;
        //        }
        //        else if (value < prevValue)
        //        {
        //            minus++;
        //            if (minus >= DirectionCheckRequired) return -1;
        //        }

        //    }

        //    return (index > 0 ? analyzer.getDataItem(index - 1).Price.Direction2D : 0);

        //}



        #region CHECKING FOR EXTREMA

        private void processExtrema(DataSet dataSet, IEnumerable<ExtremumType> types)
        {
            foreach (ExtremumType type in types)
            {
                processExtremum(dataSet, type);
            }
        }

        private void processExtremum(DataSet dataSet, ExtremumType type)
        {
            Price price = dataSet.GetPrice();
            if (price == null) throw new Exception("Price should not be null");

            Extremum extremum = price.GetExtremum(type);
            IExtremumProcessor processor = getExtremumProcessor();
            if (extremum == null)
            {
                if (price.IsNew)
                {
                    if (processor.IsExtremum(dataSet, type))
                    {
                        extremum = new Extremum(dataSet.GetAssetId(), dataSet.GetTimeframeId(), type, dataSet.GetDate()) { IndexNumber = dataSet.IndexNumber };
                        price.SetExtremum(extremum);
                    }
                }
            }

            if (extremum != null)
            {
                extremum.EarlierAmplitude = processor.CalculateEarlierAmplitude(extremum);
                extremum.EarlierCounter = processor.CalculateEarlierCounter(extremum);
                extremum.EarlierChange1 = processor.CalculateEarlierChange(extremum, 1);
                extremum.EarlierChange2 = processor.CalculateEarlierChange(extremum, 2);
                extremum.EarlierChange3 = processor.CalculateEarlierChange(extremum, 3);
                extremum.EarlierChange5 = processor.CalculateEarlierChange(extremum, 5);
                extremum.EarlierChange10 = processor.CalculateEarlierChange(extremum, 10);
                extremum.LaterAmplitude = processor.CalculateLaterAmplitude(extremum);
                extremum.LaterCounter = processor.CalculateLaterCounter(extremum);
                extremum.LaterChange1 = processor.CalculateLaterChange(extremum, 1);
                extremum.LaterChange2 = processor.CalculateLaterChange(extremum, 2);
                extremum.LaterChange3 = processor.CalculateLaterChange(extremum, 3);
                extremum.LaterChange5 = processor.CalculateLaterChange(extremum, 5);
                extremum.LaterChange10 = processor.CalculateLaterChange(extremum, 10);
                extremum.Value = processor.CalculateValue(extremum);
                extremum.IsUpdated = true;
            }

        }

        #endregion CHECKING FOR EXTREMA

    }

}