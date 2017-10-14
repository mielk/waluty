using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Utils;

namespace Stock.Domain.Entities
{
    public class ChartPoint
    {
        public int IndexNumber { get; set; }
        public double Level { get; set; }

        public ChartPoint(int indexNumber, double level)
        {
            this.IndexNumber = IndexNumber;
            this.Level = level;
        }


        #region SYSTEM.OBJECT

        public override bool Equals(object obj)
        {
            //const double MAX_VALUE_DIFFERENCE = 0.000000001d;
            if (obj == null) return false;
            if (obj.GetType() != typeof(ChartPoint)) return false;

            ChartPoint compared = (ChartPoint)obj;
            if ((compared.IndexNumber) != IndexNumber) return false;
            if (!compared.Level.IsEqual(Level)) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return IndexNumber + " | " + Level;
        }

        #endregion SYSTEM.OBJECT

    }
}
