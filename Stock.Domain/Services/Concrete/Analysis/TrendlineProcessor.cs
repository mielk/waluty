using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock.Domain.Services
{
    public class TrendlineProcessor : ITrendlineProcessor
    {
        private const int DEFAULT_MAX_DISTANCE_BETWEEN_EXTREMA = 120;
        private const int DEFAULT_MIN_DISTANCE_BETWEEN_EXTREMA = 3;
        //------------------------------------------------------------------------------------------------------------------
        private IProcessManager manager;
        //------------------------------------------------------------------------------------------------------------------
        public int MaxDistanceBetweenExtrema { get; set; }
        public int MinDistanceBetweenExtrema { get; set; }
        //------------------------------------------------------------------------------------------------------------------


        #region CONSTRUCTOR

        public TrendlineProcessor(IProcessManager manager)
        {
            this.manager = manager;
            MaxDistanceBetweenExtrema = DEFAULT_MAX_DISTANCE_BETWEEN_EXTREMA;
            MinDistanceBetweenExtrema = DEFAULT_MIN_DISTANCE_BETWEEN_EXTREMA;
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
            DataSet masterDataSet = manager.GetDataSet(group.MasterExtremum.IndexNumber);
            DataSet slaveDataSet = manager.GetDataSet(group.SecondExtremum.IndexNumber);
            List<ChartPoint> chartPoints = new List<ChartPoint>();
            return chartPoints;
        }

        
    }

}