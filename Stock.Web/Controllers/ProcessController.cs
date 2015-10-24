using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock.Domain.Services;
using Stock.Domain.Entities;

namespace Stock.Web.Controllers
{
    public class ProcessController : Controller
    {
        private readonly IProcessService processService;


        public ProcessController(IProcessService processService)
        {
            this.processService = processService;
        }


        //
        // GET: /Categories/

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult RunProcess()
        {
            var result = processService.Run();
            var json = new { value = result };
            return Json(json, JsonRequestBehavior.AllowGet);
        }




    }
}
