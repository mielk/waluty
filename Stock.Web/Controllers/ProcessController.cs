﻿using System;
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
            Asset _asset = Asset.BySymbol(asset);
            Timeframe _timeframe = Timeframe.ByName(timeframe);
            ProcessService service = new ProcessService(_asset, _timeframe);
            var types = AnalysisTypeHelper.FromStringListToTypesList(analysisTypes, ',');

            service.Setup(types);
            var result = service.Run(fromScratch);
            var json = new { value = result };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}
