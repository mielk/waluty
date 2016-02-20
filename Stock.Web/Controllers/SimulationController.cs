﻿using System;
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

        

        [HttpGet]
        [AllowAnonymous]
        public ActionResult NextStep(int incrementation)
        {

            var service = this.ssf.GetService();
            var index = service.NextStep(incrementation);            
            var json = new { index = index };
            return Json(json, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDataSetProperties()
        {
            var service = this.ssf.GetService();
            var properties = service.GetDataSetProperties();
            return Json(properties, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetFxQuotationsByDates(string startDate, string endDate)
        {

            //Converts date strings to DateTime objects.
            DateTime startDateTime = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDateTime = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            //findTrendlines(pairSymbol, timeband);

            var service = this.ssf.GetService();
            IEnumerable<DataItem> quotations = service.GetQuotations(startDateTime, endDateTime);

            return Json(quotations, JsonRequestBehavior.AllowGet);

        }




    }
}