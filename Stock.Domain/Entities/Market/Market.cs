using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Services;

namespace Stock.Domain.Entities
{
    public class Market : IJsonable
    {

        //Static properties.
        private static IMarketService service = ServiceFactory.Instance().GetMarketService();
        
        //Instance properties.
        private int id { get; set; }
        private string name { get; set; }
        private string symbol { get; set; }
        //public DateTime StartTime { get; set; }
        //public DateTime EndTime { get; set; }
        //public bool IsAroundClock { get; set; }



        #region STATIC_METHODS

        public static void injectService(IMarketService _service)
        {
            service = _service;
        }

        public static void restoreDefaultService()
        {
            service = ServiceFactory.Instance().GetMarketService();
        }

        public static IEnumerable<Market> GetMarkets()
        {
            return service.GetMarkets();
        }

        public static Market ById(int id)
        {
            return service.GetMarketById(id);
        }

        public static Market ByName(string name)
        {
            return service.GetMarketByName(name);
        }

        public static Market BySymbol(string symbol)
        {
            return service.GetMarketBySymbol(symbol);
        }

        #endregion STATIC_METHODS


        #region CONSTRUCTORS

        public Market(int id, string name, string symbol)
        {
            this.id = id;
            this.name = name;
            this.symbol = symbol;
        }

        public static Market FromDto(MarketDto dto)
        {
            var market = new Market(dto.Id, dto.Name, dto.ShortName);
            return market;
        }

        #endregion CONSTRUCTORS


        #region ACCESSORS

        public int GetId()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public string GetSymbol()
        {
            return symbol;
        }

        public object GetJson()
        {
            return new 
            {
                Id = this.id,
                Name = this.name,
                Symbol = this.symbol
            };
        }

        #endregion ACCESSORS


        #region API

        public IEnumerable<Asset> GetAssets()
        {
            return Asset.GetAssetsForMarket(id);
        }

        #endregion API


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Market)) return false;

            Market compared = (Market)obj;
            if ((compared.id) != id) return false;
            if (!compared.name.Equals(name, StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!compared.symbol.Equals(symbol, StringComparison.CurrentCultureIgnoreCase)) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
