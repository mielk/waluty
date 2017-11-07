using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class ExtremumGroup
    {
        public Extremum MasterExtremum { get; set; }
        public Extremum SecondExtremum { get; set; }
        public bool IsPeak { get; set; }

        public ExtremumGroup(Extremum master, Extremum second)
        {

            if (master == null && second == null)
            {
                throw new ArgumentNullException("Both extrema cannot be Null");
            }

            if (master != null && second != null && master.IsPeak() != second.IsPeak())
            {
                throw new ArgumentException("Extremum group cannot contain extrema of different types");
            }



            this.IsPeak = (master != null ? master.IsPeak() : second.IsPeak());
            this.MasterExtremum = (master != null ? master : second);
            this.SecondExtremum = (second != null ? second : master);
        }

        public int GetIndex()
        {
            if (MasterExtremum != null)
            {
                return MasterExtremum.GetIndexNumber();
            }
            else if (SecondExtremum != null)
            {
                return SecondExtremum.GetIndexNumber();
            }
            return 0;
        }

        public int GetMasterIndex()
        {
            return MasterExtremum.GetIndexNumber();
        }

        public int GetSlaveIndex()
        {
            return SecondExtremum.GetIndexNumber();
        }

        public int GetLateIndexNumber()
        {
            var masterIndex = (MasterExtremum != null ? MasterExtremum.GetIndexNumber() : 0);
            var secondIndex = (SecondExtremum != null ? SecondExtremum.GetIndexNumber() : 0);
            return Math.Max(masterIndex, secondIndex);
        }

        public int GetIndexNumberForQuotation(double quotation)
        {
            return GetIndex();
        }

    }

}


//using Stock.Domain.Enums;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Stock.Domain.Services;

//namespace Stock.Domain.Entities
//{
//    public class ExtremumGroup
//    {
//        private const double MIN_STEP = 0.00001d;
//        public DataItem master { get; set; }
//        public DataItem slave { get; set; }
//        public ExtremumType type { get; set; }

//        public DateTime getDate(){
//            if (master != null && (slave == null || master.Date.CompareTo(slave.Date) < 0)) return master.Date;
//            return (slave != null ? slave.Date : new DateTime());
//        }

//        public DataItem getStartItem()
//        {
//            return (slave == null || slave.Index > master.Index ? master : slave);
//        }

//        public DataItem getEndItem()
//        {
//            return (slave == null || slave.Index < master.Index ? master : slave);
//        }

//        public double getIndex() 
//        {
//            if (slave == null)
//            {
//                return master.Index;
//            }
//            else
//            {
//                return (master.Index + slave.Index) / 2;
//            }
//        }

//        public double getClosePrice()
//        {
//            return getCloseDataItem().Quotation.Close;
//        }

//        public DataItem getCloseDataItem()
//        {
//            return master;
//        }

//        public DataItem getExtremeDataItem()
//        {
//            return (slave != null ? slave : master);
//        }

//        public double getExtremePrice()
//        {
//            Quotation q = getExtremeDataItem().Quotation;
//            return (type == ExtremumType.PeakByClose || type == ExtremumType.PeakByHigh ? q.High : q.Low);
//        }

//        public ExtremumType getType()
//        {
//            return type;
//        }

//        public double getLower()
//        {
//            //if (type.IsPeak())
//            //{
//            //    return master.Quotation.Close;
//            //}
//            //else
//            //{
//            //    return getExtremePrice();
//            //}
//            return master.Quotation.Close; ;
//        }

//        public double getHigher()
//        {
//            //if (type.IsPeak())
//            //{
//            //    return getExtremePrice();
//            //}
//            //else
//            //{
//                return master.Quotation.Close;
//            //}
//        }

//        public double getStep()
//        {
//            var close = master.Quotation.Close;
//            var extreme = getExtremePrice();
//            return Math.Max(Math.Abs(close - extreme) / 10, MIN_STEP);
//        }

//        public bool isOpposite(ExtremumGroup subextremum)
//        {
//            //if (type.IsPeak() && !subextremum.type.IsPeak()) return true;
//            //if (!type.IsPeak() && subextremum.type.IsPeak()) return true;
//            return false;
//        }

//    }
//}
