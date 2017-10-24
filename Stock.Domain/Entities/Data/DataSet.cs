using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Core;

namespace Stock.Domain.Entities
{
    public class DataSet : IDataUnit, IJsonable
    {
        
        private const AnalysisType analysisType = AnalysisType.DataSet;
        public int TimeframeId { get; set; }
        public int AssetId { get; set; }
        public DateTime Date { get; set; }
        public int IndexNumber { get; set; }

        //Subitems.
        public Quotation quotation { get; set; }
        public Price price { get; set; }


        #region CONSTRUCTORS

        public DataSet(int assetId, int timeframeId, DateTime date, int indexNumber)
        {
            this.AssetId = assetId;
            this.TimeframeId = timeframeId;
            this.Date = date;
            this.IndexNumber = indexNumber;
        }

        #endregion CONSTRUCTORS


        #region GETTERS

        public Quotation GetQuotation()
        {
            return quotation;
        }

        public Price GetPrice()
        {
            return price;
        }

        public DateTime GetDate()
        {
            return Date;
        }

        public int GetIndexNumber()
        {
            return IndexNumber;
        }

        public int GetAssetId()
        {
            return AssetId;
        }

        public int GetTimeframeId()
        {
            return TimeframeId;
        }

        public AnalysisType GetAnalysisType()
        {
            return AnalysisType.DataSet;
        }

        public object GetJson()
        {
            return new
            {
                TimeframeId = TimeframeId,
                AssetId = AssetId,
                Date = Date,
                IndexNumber = IndexNumber,
                quotation = (this.quotation == null ? null : this.quotation.GetJson()),
                price = (this.price == null ? null : this.price.GetJson())
            };
        }

        public bool IsNew()
        {
            return false;
        }

        public bool IsUpdated()
        {
            return false;
        }

        #endregion GETTERS


        #region SETTERS

        public DataSet SetObject(AnalysisType analysisType, IDataUnit obj)
        {
            switch (analysisType)
            {
                case AnalysisType.Quotations: this.quotation = (Quotation)obj; break;
                case AnalysisType.Prices: this.price = (Price)obj; break;
            }
            return this;
        }

        public DataSet SetQuotation(Quotation quotation)
        {
            if (quotation.DataSet != this)
            {
                throw new ArgumentException("It is not allowed to set quotation with different DataSet to a given DataSet");
            }
            this.quotation = quotation;
            return this;
        }

        public DataSet SetPrice(Price price)
        {
            if (price.DataSet != this)
            {
                throw new ArgumentException("It is not allowed to set price with different DataSet to a given DataSet");
            }
            this.price = price;
            return this;
        }

        #endregion SETTERS


        #region SYSTEM.OBJECT

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(DataSet)) return false;

            DataSet compared = (DataSet)obj;
            if ((compared.IndexNumber) != IndexNumber) return false;
            if (compared.Date.CompareTo(Date) != 0) return false;
            if ((compared.AssetId) != AssetId) return false;
            if ((compared.TimeframeId) != TimeframeId) return false;
            if ((quotation == null && compared.GetQuotation() != null) || (quotation != null && !quotation.Equals(compared.GetQuotation()))) return false;
            if ((price == null && compared.GetPrice() != null) || (price != null && !price.Equals(compared.GetPrice()))) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Date.ToString() + " | " + TimeframeId + " | " + AssetId;
        }

        #endregion SYSTEM.OBJECT


    }
}
