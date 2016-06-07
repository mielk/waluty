using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private static IEnumerable<Market> markets = new List<Market>();


        #region static methods
        public static Market GetMarket(int id)
        {
            return null;
        }
        public static Market GetMarket(string name)
        {
            return null;
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
