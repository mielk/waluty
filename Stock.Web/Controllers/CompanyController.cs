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
        public ActionResult GetCompany(int id)
        {
            var asset = FxPair.ById(id);
            var result = Json(asset.GetJson(), JsonRequestBehavior.AllowGet); ;
            //return Json(asset.GetJson(), JsonRequestBehavior.AllowGet);
            return result;
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
        public ActionResult FilterCompanies(string q, int limit)
        {
            var assets = assetService.GetAssets(q, limit);
            var items = assets.Select(jsonAsset).ToList();
            var json = new { total = assets.Count(), items = items };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }

}
