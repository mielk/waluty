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
    public class ProcessController : Controller
    {

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult RunProcess(int simulationId)
        {

            //Simulation trendline = ServiceFactory.Instance().GetSimulationService().GetSimulationById(simulationId);
            //ISimulationManager manager = new SimulationManager(trendline);
            //service.Setup(types);
            //var result = service.Run(fromScratch);
            //var json = new { value = result };
            //return Json(json, JsonRequestBehavior.AllowGet);
            return null;
        }

    }
}
