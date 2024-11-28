using System;
using System.Linq;
using System.Web.Mvc;
using VLCitas.filters;
using VLCitas.DataLayer.CitasRepository;
using VLCitas.DataLayer.OfficesRepository;
using VLCitas.DataLayer;

namespace VLCitas.Controllers
{
    public class ConsultorioController : MyController
    {
        // GET: Citas
        private VL_CitasEntities db;
        private CitaRepository citaRepo;

        public ConsultorioController()
        {
            db = new VL_CitasEntities();
            citaRepo = new CitaRepository();
        }

        [GeneralAcces]
        public ActionResult Index(Guid consultory)
        {
            Session["consultory_uid"] = null;
            Offices_model office = (Offices_model)Session["office"];
            var item = db.Get_TotalCitasByStatusxConsultories(office.uId).ToList();
            ViewBag.item = db.GetInvoicesByOfficeConsultory(office.uId).OrderByDescending(o => o.cita_date).ToList().Take(50);
            ViewBag.name = office.name;
            ViewBag.statics = db.GetCountCitasByConsultory(office.uId).ToList();
            ViewBag.month = db.GetMonthCitas(office.uId).ToList();
            ViewBag.status = db.GetCitasByStatus(office.uId).ToList();
            var list = db.GetGananciaByMonth(office.uId).ToList();
            ViewBag.datapie = list;
            return View(item);
        }
    }
}