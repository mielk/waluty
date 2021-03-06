﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock.Domain.Services;
using Stock.Domain.Enums;
using Stock.Domain.Entities;
using Stock.Core;

namespace Stock.Web.Controllers
{
    public class DataController : Controller
    {

        private static IDataSetService dataSetService = ServiceFactory.Instance().GetDataSetService();
        //private static int AssetId;
        //private static int TimeframeId;
        //private static Simulation trendBreak;
        //private static IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };
        //private static ISimulationManager manager;

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult GetDataSetsInfo(int AssetId, int TimeframeId, int simulationId)
        //{
        //    AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(AssetId, TimeframeId) { SimulationId = simulationId };
        //    AnalysisInfo analysisInfo = dataSetService.GetAnalysisInfo(queryDef, AnalysisType.Quotations);
        //    var json = new { value = analysisInfo };
        //    return Json(json, JsonRequestBehavior.AllowGet);
        //}


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDataSets(int assetId, int timeframeId, int simulationId, int? startIndex, int? endIndex)
        {

            //Simulation trendBreak = ServiceFactory.Instance().GetSimulationService().GetSimulationById(simulationId);
            //IProcessManager manager = new ProcessManager(trendBreak);
            //service.Setup(types);
            //var result = service.Run(fromScratch);
            //var json = new { value = result };
            //return Json(json, JsonRequestBehavior.AllowGet);
            return null;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDataSetsInfo(int assetId, int timeframeId, int? startIndex, int? endIndex)
        {

            IProcessManager manager = new ProcessManager(assetId, timeframeId);
            AnalysisInfo info = manager.GetAnalysisInfo();
            if (info == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new
                {
                    startIndex = info.StartIndex,
                    endIndex = info.EndIndex,
                    firstDate = info.StartDate,
                    lastDate = info.EndDate,
                    minLevel = info.MinLevel,
                    maxLevel = info.MaxLevel,
                    counter = info.Counter
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }



        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult GetQuotations(int AssetId, int TimeframeId, int count)
        //{
        //    //AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(AssetId, TimeframeId) { Limit = count };
        //    //IEnumerable<DataSet> dataSets = dataService.GetDataSets(queryDef);
        //    //IEnumerable<Quotation> quotations = dataSets.Select(price => price.GetQuotation());
        //    //IEnumerable<Trendline> trendRanges = null;
        //    //var result = new { quotations = dataSets, trendRanges = trendRanges };
        //    //return Json(result, JsonRequestBehavior.AllowGet);
        //    return null;
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult GetQuotationsByDates(int AssetId, int TimeframeId, string startDate, string endDate)
        //{

        //    //Converts date strings to DateTime objects.
        //    DateTime startDateTime = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        //    DateTime endDateTime = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        //    AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(AssetId, TimeframeId) { StartDate = startDateTime, EndDate = endDateTime };
        //    IEnumerable<DataSet> dataSets = dataService.GetDataSets(queryDef);
        //    //IEnumerable<Trendline> trendRanges = null;
        //    //var result = new { quotations = dataSets, trendRanges = trendRanges };

        //    //return Json(result, JsonRequestBehavior.AllowGet);
        //    return null;

        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult GetDataSetProperties(int AssetId, int TimeframeId)
        //{
        //    //AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(AssetId, TimeframeId);
        //    //queryDef.StartDate = new DateTime(2017, 5, 1, 12, 0, 0);
        //    //AnalysisInfo info = dataService.GetAnalysisInfo(queryDef, AnalysisType.Quotations);
        //    //var result = new { firstDate = info.StartDate, lastDate = info.EndDate, minLevel = info.MinLevel, maxLevel = info.MaxLevel, counter = info.Counter};
        //    //return Json(result, JsonRequestBehavior.AllowGet);
        //    return null;
        //}

    }
}
