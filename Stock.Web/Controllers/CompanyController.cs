using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock.Domain.Services;
using Stock.Domain.Entities;

namespace Stock.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly IFxService fxService;
        private readonly IDataService dataService;
        private readonly ITrendService trendService;


        public CompanyController(ICompanyService companyService, IDataService dataService, 
                                 IFxService fxService, ITrendService trendService)
        {
            this.companyService = companyService;
            this.dataService = dataService;
            this.fxService = fxService;
            this.trendService = trendService;
        }

        //
        // GET: /Categories/

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult FilterCompanies(string q, int limit)
        {
            //var companies = companyService.FilterCompanies(q, limit);
            var assets = fxService.FilterPairs(q, limit);
            var items = assets.Select(jsonAsset).ToList();
            var json = new { total = assets.Count(), items = items };
            return Json(json, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetCompany(int id)
        {
            //var company = companyService.GetCompany(id);
            var asset = fxService.GetPair(id);
            return Json(asset, JsonRequestBehavior.AllowGet);
        }

        private static object jsonAsset(Asset asset)
        {
            return new
            {
                id = asset.Id,
                name = asset.Name,
                asset = asset
            };
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetQuotations(int companyId, int timeband, int count)
        {

            IEnumerable<Quotation> quotations;
            if (count > 0)
            {
                quotations = dataService.GetQuotations(companyId, timeband, count);
            } 
            else 
            {
                quotations = dataService.GetQuotations(companyId, timeband);
            }

            return Json(quotations, JsonRequestBehavior.AllowGet);

        }




        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetFxQuotations(string pairSymbol, string timeband, int count)
        {

            //findTrendlines(pairSymbol, timeband);

            string symbol = pairSymbol + "_" + timeband;
            IEnumerable<DataItem> quotations = (count > 0 ? 
                                    dataService.GetFxQuotations(symbol, count) : 
                                    dataService.GetFxQuotations(symbol));

            return Json(quotations, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDataSetProperties(string pairSymbol, string timeband)
        {
            string symbol = pairSymbol + "_" + timeband;
            var properties = dataService.GetDataSetProperties(symbol);
            return Json(properties, JsonRequestBehavior.AllowGet);
        }

    }
}
