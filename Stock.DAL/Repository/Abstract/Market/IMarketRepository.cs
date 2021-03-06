﻿using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public interface IMarketRepository
    {
        IEnumerable<MarketDto> GetMarkets();
        MarketDto GetMarketById(int id);
        MarketDto GetMarketByName(string name);
        MarketDto GetMarketBySymbol(string symbol);
    }
}
