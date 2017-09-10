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
        
        //public SimulationController()
        //{
        //    this.ssf = null;// serviceFactory;
        //}

        //public ActionResult Index()
        //{
        //    return View();
        //}


        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult InitializeSimulation(string pair, string timeframe)

        //    //Później dodać, żeby można było podać jako parametr wejściowy datę, od której ma się rozpoczynać
        //    //symulacja. Na razie wszystkie symulacje rozpoczynają się od początku.
        //    //Kolejny parametr do dodania to jakie analizy mają być wykonane. Obecnie domyślnie robi tylko PRICE.
        //{

        //    //Restart simulation object.
        //    var service = this.ssf.GetService();
        //    var types = new AnalysisType[] { AnalysisType.Prices, AnalysisType.Trendlines };
        //    var result = service.Start(pair, timeframe, types);

        //    var json = new { pair = pair, timeframe = timeframe, result = result };

        //    Debug.WriteLine(string.Format("+;Initializing simulation [{0}, {1}]", pair, timeframe));

        //    return Json(json, JsonRequestBehavior.AllowGet);

        //}

        

        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult NextStep(int incrementation)
        //{

        //    Debug.WriteLine("---------------------------------------------------------------------------");
        //    Debug.WriteLine("+;<SimulationController.NextStep>");

        //    var service = this.ssf.GetService();
        //    var index = service.NextStep(incrementation);            

        //    var json = new { index = index };

        //    Debug.WriteLine("+;<///SimulationController.NextStep>");

        //    return Json(json, JsonRequestBehavior.AllowGet);

        //}


        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult GetDataSetProperties()
        //{
        //    var service = this.ssf.GetService();
        //    var properties = service.GetDataSetProperties();
        //    return Json(properties, JsonRequestBehavior.AllowGet);
        //}



        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult GetFxQuotationsByDates(string startDate, string endDate)
        //{

        //    //Converts date strings to DateTime objects.
        //    DateTime startDateTime = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        //    DateTime endDateTime = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        //    //findTrendlines(pairSymbol, timeframe);

        //    ISimulationService2 service = this.ssf.GetService();
        //    IEnumerable<DataItem> quotations = service.GetQuotations(startDateTime, endDateTime);
        //    IEnumerable<Trendline> trendlines = service.GetTrendlines(startDateTime, endDateTime);
        //    var result = new { quotations = quotations, trendlines = trendlines };

        //    return Json(result, JsonRequestBehavior.AllowGet);

        //}

    }
}