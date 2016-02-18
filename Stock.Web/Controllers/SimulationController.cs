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
        public ActionResult InitializeSimulation(string pair, string timeband)
        {

            //Restart simulation object.
            var service = this.ssf.GetService();
            var result = service.Start(pair, timeband);

            var json = new { pair = pair, timeband = timeband, result = result };
            return Json(json, JsonRequestBehavior.AllowGet);

        }


    }
}