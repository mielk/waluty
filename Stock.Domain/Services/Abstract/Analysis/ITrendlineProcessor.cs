using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface ITrendlineProcessor
    {
        bool CanCreateTrendline(ExtremumGroup baseGroup, ExtremumGroup footholdGroup);
        IEnumerable<Trendline> GetTrendlines(ExtremumGroup baseGroup, ExtremumGroup footholdGroup);
        IEnumerable<ChartPoint> GetChartPoints(ExtremumGroup group);
    }
}
