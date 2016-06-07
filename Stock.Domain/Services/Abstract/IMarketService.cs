using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.Domain.Services
{
    public interface IMarketService
    {

                                                                        #region markets
        IEnumerable<Market> GetMarkets();
                                                                        #endregion markets


                                                                        #region assets
        IEnumerable<Company> FilterCompanies(string q, int limit);
        Company GetCompany(int id);
                                                                        #endregion assets


                                                                        #region fx
        IEnumerable<FxPair> FilterPairs(string q, int limit);
        FxPair GetPair(int id);
        FxPair GetPair(string symbol);
                                                                        #endregion fx

    }
}
