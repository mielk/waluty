using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.Core;

namespace Stock.Domain.Services
{
    public class TrendlineProcessor : ITrendlineProcessor
    {
        private const int DEFAULT_MAX_DISTANCE_BETWEEN_EXTREMA = 120;
        private const int DEFAULT_MIN_DISTANCE_BETWEEN_EXTREMA = 3;
        private const int DEFAULT_MAX_CHART_POINTS_FOR_EXTREMUM_GROUP = 6;
        private const double DEFAULT_MIN_DISTANCE_BETWEEN_CHART_POINTS = 0.0001;
        //------------------------------------------------------------------------------------------------------------------
        private IProcessManager manager;
        //------------------------------------------------------------------------------------------------------------------
        public int MaxDistanceBetweenExtrema { get; set; }
        public int MinDistanceBetweenExtrema { get; set; }
        public int MaxChartPointsForExtremumGroup { get; set; }
        public double MinDistanceBetweenChartPoints { get; set; }
        //Single processing ------------------------------------------------------------------------------------------------
        private int index;

        //------------------------------------------------------------------------------------------------------------------


        #region CONSTRUCTOR

        public TrendlineProcessor(IProcessManager manager)
        {
            this.manager = manager;
            MaxDistanceBetweenExtrema = DEFAULT_MAX_DISTANCE_BETWEEN_EXTREMA;
            MinDistanceBetweenExtrema = DEFAULT_MIN_DISTANCE_BETWEEN_EXTREMA;
            MaxChartPointsForExtremumGroup = DEFAULT_MAX_CHART_POINTS_FOR_EXTREMUM_GROUP;
            MinDistanceBetweenChartPoints = DEFAULT_MIN_DISTANCE_BETWEEN_CHART_POINTS;
        }

        #endregion CONSTRUCTOR


        #region SETTERS



        #endregion SETTERS


        public bool CanCreateTrendline(ExtremumGroup baseGroup, ExtremumGroup footholdGroup)
        {
            var difference = footholdGroup.GetIndex() - baseGroup.GetIndex();
            if (difference > MaxDistanceBetweenExtrema)
            {
                return false;
            }
            else if (difference < MinDistanceBetweenExtrema)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<Trendline> GetTrendlines(ExtremumGroup baseGroup, ExtremumGroup footholdGroup)
        {
            AtsSettings settings = manager.GetSettings();
            List<Trendline> list = new List<Trendline>();
            IEnumerable<ChartPoint> baseChartPoints = GetChartPoints(baseGroup);
            IEnumerable<ChartPoint> footholdChartPoints = GetChartPoints(footholdGroup);

            foreach (var baseChartPoint in baseChartPoints)
            {
                foreach (var footholdChartPoint in footholdChartPoints)
                {
                    TrendlinePoint basePoint = new TrendlinePoint(baseGroup, baseChartPoint.Level);
                    TrendlinePoint footholdPoint = new TrendlinePoint(footholdGroup, footholdChartPoint.Level);
                    Trendline trendline = new Trendline(settings, basePoint, footholdPoint);
                    list.Add(trendline);
                }
            }

            return list;

        }

        public IEnumerable<ChartPoint> GetChartPoints(ExtremumGroup group)
        {

            DataSet masterDataSet = manager.GetDataSet(group.MasterExtremum.GetIndexNumber());
            DataSet slaveDataSet = manager.GetDataSet(group.SecondExtremum.GetIndexNumber());
            List<ChartPoint> chartPoints = new List<ChartPoint>();
            bool isPeak = group.IsPeak;
            double distance = (isPeak ? slaveDataSet.quotation.High : slaveDataSet.quotation.Low) - masterDataSet.quotation.Close;
            double singleStep = (distance > MaxChartPointsForExtremumGroup * MinDistanceBetweenChartPoints ? (distance / (MaxChartPointsForExtremumGroup - 1)) : MinDistanceBetweenChartPoints);
            double limitValue = (isPeak ? slaveDataSet.quotation.High : slaveDataSet.quotation.Low);

            var upLevel = isPeak ? slaveDataSet.quotation.High : masterDataSet.quotation.Close;
            var downLevel = isPeak ? masterDataSet.quotation.Close : slaveDataSet.quotation.Low;

            chartPoints.Add(new ChartPoint(group.GetIndexNumberForQuotation(upLevel), upLevel));
            chartPoints.Add(new ChartPoint(group.GetIndexNumberForQuotation(downLevel), downLevel));

            while (upLevel > downLevel) 
            {
                upLevel = upLevel - singleStep;
                if (upLevel > downLevel)
                {
                    downLevel = downLevel + singleStep;
                    var upChartPoint = new ChartPoint(group.GetIndexNumberForQuotation(upLevel), upLevel);
                    chartPoints.Add(upChartPoint);
                    var downChartPoint = new ChartPoint(group.GetIndexNumberForQuotation(upLevel), downLevel);
                    chartPoints.Add(downChartPoint);
                }
            }

            return chartPoints;
        }


        public void Process(Trendline trendline, IEnumerable<ExtremumGroup> extremumGroups)
        {

            var lastQuotationIndex = manager.GetAnalysisLastUpdatedIndex(AnalysisType.Quotations);
            if (lastQuotationIndex > trendline.LastUpdateIndex)
            {
                var range = trendline.CurrentIsPeak;
            }

            //trendline.LastUpdateIndex = this.lastQuotationIndex;
            var x = 1;

        }

        

    }

}