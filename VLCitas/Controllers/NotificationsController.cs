using VLCitas.DataLayer.CitasRepository;
using VLCitas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VLCitas.Controllers
{
    public class NotificationsController : Controller
    {
        private CitaRepository citaRepo;
        public NotificationsController()
        {
            citaRepo = new CitaRepository();
        }

        [HttpGet]
        public JsonResult SendAlerts()
        {
            string path = HttpContext.Request.Url.Authority;
            return Json(citaRepo.SendAlerts(path), JsonRequestBehavior.AllowGet);
        }

       
    }
}
