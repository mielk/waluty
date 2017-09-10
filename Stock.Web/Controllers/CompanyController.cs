using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock.Domain.Services;
using Stock.Domain.Entities;
using Stock.Core;

namespace Stock.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IAssetService assetService;
        private readonly IDataSetService dataService;


        public CompanyController(IDataSetService dataService)
        {
            this.assetService = ServiceFactory.Instance().GetAssetService();
            this.dataService = dataService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult FilterCompanies(string q, int limit)
        {
            var assets = assetService.GetAssets(q, limit);
            var items = assets.Select(jsonAsset).ToList();
            var json = new { total = assets.Count(), items = items };
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetCompany(int id)
        {
            var asset = FxPair.ById(id);
            return Json(asset, JsonRequestBehavior.AllowGet);
        }

        private static object jsonAsset(Asset asset)
        {
            return new
            {
                id = asset.GetId(),
                name = asset.GetSymbol(),
                asset = asset
            };
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetQuotations(int assetId, int timeframeId, int count)
        {
            //AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(assetId, timeframeId) { Limit = count };
            //IEnumerable<DataSet> dataSets = dataService.GetDataSets(queryDef);
            //IEnumerable<Quotation> quotations = dataSets.Select(ds => ds.GetQuotation());
            //IEnumerable<Trendline> trendlines = null;
            //var result = new { quotations = dataSets, trendlines = trendlines };
            //return Json(result, JsonRequestBehavior.AllowGet);
            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetQuotationsByDates(int assetId, int timeframeId, string startDate, string endDate)
        {

            //Converts date strings to DateTime objects.
            DateTime startDateTime = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDateTime = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(assetId, timeframeId) { StartDate = startDateTime, EndDate = endDateTime };
            IEnumerable<DataSet> dataSets = dataService.GetDataSets(queryDef);
            //IEnumerable<Trendline> trendlines = null;
            //var result = new { quotations = dataSets, trendlines = trendlines };

            //return Json(result, JsonRequestBehavior.AllowGet);
            return null;

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDataSetProperties(int assetId, int timeframeId)
        {
            //AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(assetId, timeframeId);
            //queryDef.StartDate = new DateTime(2017, 5, 1, 12, 0, 0);
            //AnalysisInfo info = dataService.GetAnalysisInfo(queryDef, AnalysisType.Quotations);
            //var result = new { firstDate = info.StartDate, lastDate = info.EndDate, minLevel = info.MinLevel, maxLevel = info.MaxLevel, counter = info.Counter};
            //return Json(result, JsonRequestBehavior.AllowGet);
            return null;
        }

    }

}
