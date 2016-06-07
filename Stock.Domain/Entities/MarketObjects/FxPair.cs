using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class FxPair : Asset
    {
        public bool IsFx { get; set; }
        public int BaseCurrency { get; set; }
        public int QuoteCurrency { get; set; }


        public FxPair(int id, string name, Currency baseCurrency, Currency quoteCurrency) : base(id, name)
        {
            assignCurrencies(baseCurrency.Id, quoteCurrency.Id);
        }

        public FxPair(int id, string name, int baseCurrencyId, int quoteCurrencyId) : base(id, name)
        {
            assignCurrencies(baseCurrencyId, quoteCurrencyId);
        }

        private void assignCurrencies(int baseCurrency, int quoteCurrency)
        {
            IsFx = true;
            this.BaseCurrency = BaseCurrency;
            this.QuoteCurrency = QuoteCurrency;
        }




        public static FxPair FromDto(PairDto dto)
        {

            var pair = new FxPair(dto.Id, dto.PairName, dto.BaseCurrency, dto.QuoteCurrency);
            pair.IsFx = true;

            return pair;

        }


    }
}