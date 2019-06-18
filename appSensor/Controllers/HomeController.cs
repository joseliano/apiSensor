using appSensor.Models;
using appSensor.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace appSensor.Controllers
{
    public class HomeController : Controller
    {
        static readonly IEntity<RegiaoModels> sRegiao = new RegiaoRepository();
        static readonly IEntity<PaisModels> sPais = new PaisRepository();
        static readonly IEntity<SensorModels> sSensor = new SensorRepository();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GraficoRegiao()
        {
            var iDados = sRegiao.ListAll();
            return Json(iDados, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GraficoPais()
        {
            var iDados = sPais.ListAll();
            return Json(iDados, JsonRequestBehavior.AllowGet);             
        }

        [HttpGet]
        public JsonResult listPais()
        {
            var iDados = sPais.ListAll(false);
            return Json(iDados, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listSensor()
        {
            var iDados = sSensor.ListAll();
            return Json(iDados, JsonRequestBehavior.AllowGet);
        }

    }
}