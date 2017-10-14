using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class TrendlinePoint
    {
        public ExtremumGroup Group { get; set; }
        public double Level { get; set; }

        public TrendlinePoint(ExtremumGroup group, double level)
        {
            this.Group = group;
            this.Level = level;
        }

        public int GetIndex()
        {
            return this.Group.GetIndex();
        }

    }

}