using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock.Domain.Services;

namespace Stock.Web.Controllers
{

    public class MarketController : Controller
    {

        private readonly IMarketService _service;


        public MarketController()
        {
            _service = ServiceFactory.GetMarketService();
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
            var markets = _service.GetMarkets();
            return Json(markets, JsonRequestBehavior.AllowGet);
        }

    }

}
