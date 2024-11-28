using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLCitas.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer;
using VLCitas.DataLayer.OfficesRepository;

namespace VLCitas.Controllers
{
    public class HomeController : Controller
    {
        private VL_CitasEntities db = new VL_CitasEntities();
        public ActionResult Index()
        {
            try
            {
                Schedule schedules = (Schedule)Session["schedule"];
                Consultory dep = (Consultory)Session["consultory"];
                Offices_model off = (Offices_model)Session["office"];
                ViewBag.start = schedules.start_hour;
                ViewBag.end = schedules.end_hour;
                ViewBag.duration = dep.time_prom;
                var res = db.GetCalendarCitas(off.uId).Select(x => new
                {
                    id = x.id,
                    title = x.title,
                    start = x.start.Value.ToString("yyyy-MM-ddThh:mm:ss"),
                    end = x.end.Value.ToString("yyyy-MM-ddThh:mm:ss")
                }).ToList();
                ViewBag.item = res;
                return View();
            }
             catch (Exception ex)
            {
                Common.Set_Log_Errors("Home Index Error: " + ex.ToString());
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Proximamente()
        {
            return View();
        }

    }
}