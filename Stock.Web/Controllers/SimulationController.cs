using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock.Domain.Services;
using Stock.Domain.Entities;


namespace Stock.Web.Controllers
{
    public class SimulationController : Controller
    {

        private readonly ISimulationServiceFactory ssf;
        //private readonly ISimulationService simulationService;

        public SimulationController(ISimulationServiceFactory serviceFactory)
        {
            this.ssf = serviceFactory;
        }

        //
        // GET: /Simulation/

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult RunProcess(string pair, string timeband, int value)
        {

            this.ssf.GetService().Increment();

            string symbol = pair + "_" + timeband;
            //IEnumerable<DataItem> quotations = dataService.GetFxQuotations(symbol, startDateTime, endDateTime);
            //return Json(quotations, JsonRequestBehavior.AllowGet);

            var json = new { pair = pair, timeband = timeband, value = this.ssf.GetService().GetValue() };
            return Json(json, JsonRequestBehavior.AllowGet);
        }


    }
}