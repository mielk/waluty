using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock.Domain.Services;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System.Diagnostics;
using Stock.Core;

namespace Stock.Web.Controllers
{
    public class SimulationController : Controller
    {

        private static int assetId;
        private static int timeframeId;
        private static Simulation simulation;
        private static IEnumerable<AnalysisType> analysisTypes = new AnalysisType[] { AnalysisType.Prices };
        private static ISimulationManager manager;


        public SimulationController()
        {
            
            //this.ssf = null;// serviceFactory;
        }

        public ActionResult Index()
        {
            return View();
        }



        //Później dodać, żeby można było podać jako parametr wejściowy datę, od której ma się rozpoczynać
        //symulacja. Na razie wszystkie symulacje rozpoczynają się od początku.
        //Kolejny parametr do dodania to jakie analizy mają być wykonane. Obecnie domyślnie robi tylko PRICE.
        [HttpGet]
        [AllowAnonymous]
        public ActionResult InitializeSimulation(int assetId, int timeframeId)
        {

            //Restart simulation object.
            simulation = new Simulation() { 
                Id = 1,
                Name = "test"
            };

            manager = new SimulationManager(assetId, timeframeId, simulation);
            var json = new { result = (simulation != null) };
            return Json(json, JsonRequestBehavior.AllowGet);

        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult NextStep(int incrementation)
        {
            manager.RunByGivenSteps(incrementation);
            AnalysisInfo info = manager.GetAnalysisInfo();
            var json = new { info = info };
            return Json(json, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDataSets()
        {
            IEnumerable<DataSet> dataSets = manager.GetDataSets();
            var json = new { quotations = dataSets };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDataSetsInfo()
        {

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


    }
}