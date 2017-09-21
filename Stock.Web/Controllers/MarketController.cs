using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock.Domain.Services;
using Stock.Domain.Entities;

namespace Stock.Web.Controllers
{

    public class MarketController : Controller
    {

        private readonly IMarketService _service;


        public MarketController()
        {
            _service = ServiceFactory.Instance().GetMarketService();
        }


        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetMarkets()
        {
            var markets = _service.GetMarkets().ToArray();
            JsonResult result = convertToJsonResult(markets);
            return result;
        }

        private JsonResult convertToJsonResult(IEnumerable<Market> markets)
        {
            //Json(markets);
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            List<object> items = new List<object>();
            foreach (Market market in markets)
            {
                items.Add(market.GetJson());
            }
            result.Data = items.ToArray();
            return result;
        }

    }

}
