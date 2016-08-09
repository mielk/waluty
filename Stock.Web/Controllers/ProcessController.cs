using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock.Domain.Services;
using Stock.Domain.Enums;
using Stock.Domain.Entities;
using Stock.Domain.Services.Factories;

namespace Stock.Web.Controllers
{
    public class ProcessController : Controller
    {
        //private readonly IProcessService processService;


        //public ProcessController(IProcessService processService)
        //{
        //    this.processService = processService;
        //}


        //
        // GET: /Categories/

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult RunProcess(string asset, string timeframe, bool fromScratch, string analysisTypes)
        {
            

            //Convert the given names of analysis into enumerations.
            Asset _asset = Asset.GetAssetByName(asset);
            Timeframe _timeframe = Timeframe.GetTimeframeByShortName(timeframe);
            IProcessService service = new ProcessService(_asset, _timeframe);
            var types = AnalysisTypeHelper.StringToTypesList(analysisTypes, ',');

            service.Setup(types);
            var result = service.Run(fromScratch);
            var json = new { value = result };
            return Json(json, JsonRequestBehavior.AllowGet);
        }




    }
}
