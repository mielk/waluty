using Stock.DAL.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Repositories
{
    public interface IMarketRepository
    {

                                                #region markets
        IEnumerable<MarketDto> GetMarkets();
                                                #endregion markets


                                                #region assets
        IEnumerable<CompanyDto> FilterCompanies(string q, int limit);
        CompanyDto GetCompany(int id);
                                                #endregion assets


                                                #region fx
        IEnumerable<PairDto> FilterPairs(string q, int limit);
        PairDto GetPair(int id);
        PairDto GetPair(string symbol);
                                                #endregion fx

    }
}
