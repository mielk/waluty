﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock.Domain.Services;
using Stock.Domain.Entities;
using Stock.Domain.Services.Factories;

namespace Stock.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IMarketService marketService;
        private readonly IDataService dataService;


        public CompanyController(IDataService dataService)
        {
            this.marketService = MarketServiceFactory.CreateService();
            this.dataService = dataService;
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
            var assets = marketService.FilterPairs(q, limit);
            var items = assets.Select(jsonAsset).ToList();
            var json = new { total = assets.Count(), items = items };
            return Json(json, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetCompany(int id)
        {
            //var company = companyService.GetCompany(id);
            var asset = marketService.GetFxPair(id);
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
        public ActionResult GetQuotations(int companyId, int timeframe, int count)
        {

            IEnumerable<Quotation> quotations;
            if (count > 0)
            {
                quotations = dataService.GetQuotations(companyId, timeframe, count);
            } 
            else 
            {
                quotations = dataService.GetQuotations(companyId, timeframe);
            }
            IEnumerable<Trendline> trendlines = null;

            var result = new { quotations = quotations, trendlines = trendlines };

            return Json(result, JsonRequestBehavior.AllowGet);

        }




        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetFxQuotations(string pairSymbol, string timeframe, int count)
        {

            //findTrendlines(pairSymbol, timeframe);

            string symbol = pairSymbol + "_" + timeframe;
            IEnumerable<DataItem> quotations = (count > 0 ? 
                                    dataService.GetFxQuotations(symbol, count) : 
                                    dataService.GetFxQuotations(symbol));
            IEnumerable<Trendline> trendlines = null;

            var result = new { quotations = quotations, trendlines = trendlines };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetFxQuotationsByDates(string pairSymbol, string timeframe, string startDate, string endDate)
        {

            //Converts date strings to DateTime objects.
            DateTime startDateTime = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDateTime = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            //findTrendlines(pairSymbol, timeframe);



            string symbol = pairSymbol + "_" + timeframe;
            IEnumerable<DataItem> quotations = dataService.GetFxQuotations(symbol, startDateTime, endDateTime);
            IEnumerable<Trendline> trendlines = null;
            var result = new { quotations = quotations, trendlines = trendlines };

            return Json(result, JsonRequestBehavior.AllowGet);

        }




        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDataSetProperties(string pairSymbol, string timeframe)
        {
            string symbol = pairSymbol + "_" + timeframe;
            var properties = dataService.GetDataSetProperties(symbol);
            return Json(properties, JsonRequestBehavior.AllowGet);
        }

    }
}
