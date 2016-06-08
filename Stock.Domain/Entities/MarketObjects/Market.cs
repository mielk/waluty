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
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
        //Static.
        private static IMarketService service = MarketServiceFactory.CreateService();
        private static IEnumerable<Market> markets = new List<Market>();


        #region static methods

        public static void injectService(IMarketService _service)
        {
            service = _service;
        }

        public static Market GetMarketById(int id)
        {

            var market = markets.SingleOrDefault(m => m.Id == id);

            if (market == null)
            {
                market = service.GetMarketById(id);
                if (market != null)
                {
                    markets = markets.Concat(new[] { market });
                }
            }

            return market;

        }

        public static Market GetMarketByName(string name)
        {
            
            var market = markets.SingleOrDefault(m => m.Name.Equals(name));

            if (market == null)
            {
                market = service.GetMarketByName(name);
                if (market != null)
                {
                    markets = markets.Concat(new[] { market });
                }
            }

            return market;

        }

        public static Market GetMarketBySymbol(string symbol)
        {

            var market = markets.SingleOrDefault(m => m.ShortName.Equals(symbol));

            if (market == null)
            {
                market = service.GetMarketBySymbol(symbol);
                if (market != null)
                {
                    markets = markets.Concat(new[] { market });
                }
            }

            return market;

        }

        #endregion static methods




        public Market(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.Assets = new List<Asset>();
        }


        public void setTimes(string startTime, string endTime)
        {

        }

        public void setTimes(DateTime startTime, DateTime endTime)
        {
            this.StartTime = startTime;
            this.EndTime = EndTime;
        }


        public static Market FromDto(MarketDto dto)
        {

            var market = new Market(dto.Id, dto.Name);
            market.ShortName = dto.ShortName;
            market.setTimes(dto.StartTime, dto.EndTime);
            return market;

        }

    }
}
