using System;
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

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult GetDataSetsInfo(int assetId, int timeframeId, int simulationId)
        //{
        //    AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(assetId, timeframeId) { SimulationId = simulationId };
        //    AnalysisInfo analysisInfo = dataSetService.GetAnalysisInfo(queryDef, AnalysisType.Quotations);
        //    var json = new { value = analysisInfo };
        //    return Json(json, JsonRequestBehavior.AllowGet);
        //}


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDataSets(int assetId, int timeframeId, int simulationId, int? startIndex, int? endIndex)
        {

            //Simulation simulation = ServiceFactory.Instance().GetSimulationService().GetSimulationById(simulationId);
            //IProcessManager manager = new ProcessManager(simulation);
            //service.Setup(types);
            //var result = service.Run(fromScratch);
            //var json = new { value = result };
            //return Json(json, JsonRequestBehavior.AllowGet);
            return null;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDataSetsWithInfo(int assetId, int timeframeId, int? startIndex, int? endIndex)
        {

            IProcessManager manager = new ProcessManager();
            AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(assetId, timeframeId)
            {
                SimulationId = 0,
                StartIndex = startIndex,
                EndIndex = endIndex,
                AnalysisTypes = new AnalysisType[] { AnalysisType.Quotations }
            };

            var result = manager.GetDataSets(queryDef);
            //var json = new { value = result };
            //return Json(json, JsonRequestBehavior.AllowGet);
            return null;
        }



        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult GetQuotations(int assetId, int timeframeId, int count)
        //{
        //    //AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(assetId, timeframeId) { Limit = count };
        //    //IEnumerable<DataSet> dataSets = dataService.GetDataSets(queryDef);
        //    //IEnumerable<Quotation> quotations = dataSets.Select(ds => ds.GetQuotation());
        //    //IEnumerable<Trendline> trendlines = null;
        //    //var result = new { quotations = dataSets, trendlines = trendlines };
        //    //return Json(result, JsonRequestBehavior.AllowGet);
        //    return null;
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult GetQuotationsByDates(int assetId, int timeframeId, string startDate, string endDate)
        //{

        //    //Converts date strings to DateTime objects.
        //    DateTime startDateTime = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        //    DateTime endDateTime = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        //    AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(assetId, timeframeId) { StartDate = startDateTime, EndDate = endDateTime };
        //    IEnumerable<DataSet> dataSets = dataService.GetDataSets(queryDef);
        //    //IEnumerable<Trendline> trendlines = null;
        //    //var result = new { quotations = dataSets, trendlines = trendlines };

        //    //return Json(result, JsonRequestBehavior.AllowGet);
        //    return null;

        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult GetDataSetProperties(int assetId, int timeframeId)
        //{
        //    //AnalysisDataQueryDefinition queryDef = new AnalysisDataQueryDefinition(assetId, timeframeId);
        //    //queryDef.StartDate = new DateTime(2017, 5, 1, 12, 0, 0);
        //    //AnalysisInfo info = dataService.GetAnalysisInfo(queryDef, AnalysisType.Quotations);
        //    //var result = new { firstDate = info.StartDate, lastDate = info.EndDate, minLevel = info.MinLevel, maxLevel = info.MaxLevel, counter = info.Counter};
        //    //return Json(result, JsonRequestBehavior.AllowGet);
        //    return null;
        //}

    }
}
