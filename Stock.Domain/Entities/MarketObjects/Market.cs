using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Services.Factories;
using Stock.Domain.Services;

namespace Stock.Domain.Entities
{
    public class Market
    {

        //Static properties.
        private static IMarketService service = ServiceFactory.GetMarketService();
        
        //Instance properties.
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
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
            service = ServiceFactory.GetMarketService();
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
            this.Id = id;
            this.Name = name;
            this.Symbol = symbol;
        }

        public static Market FromDto(MarketDto dto)
        {
            var market = new Market(dto.Id, dto.Name, dto.ShortName);
            return market;
        }

        #endregion CONSTRUCTORS


        #region API

        public IEnumerable<Asset> GetAssets()
        {
            return Asset.GetAssetsForMarket(Id);
        }

        #endregion API


        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Market)) return false;

            Market compared = (Market)obj;
            if ((compared.Id) != Id) return false;
            if (!compared.Name.Equals(Name, StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!compared.Symbol.Equals(Symbol, StringComparison.CurrentCultureIgnoreCase)) return false;
            return true;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
