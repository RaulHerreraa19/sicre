using System;
using System.Linq;
using System.Web.Mvc;
using VLCitas.Models;
using VLCitas.filters;
using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.CitasRepository;
using VLCitas.DataLayer;
using VLCitas.DataLayer.OfficesRepository;

namespace VLCitas.Controllers
{
    [GeneralAcces]
    public class AsistenteController : MyController
    {
        // GET: Asistente
        private Report repoReport;
        private CitaRepository citasRepo;

        public AsistenteController()
        {
            repoReport = new Report();
            citasRepo = new CitaRepository();
        }
        private VL_CitasEntities db = new VL_CitasEntities();

        public ActionResult Index()
        {
            Session["consultory_uid"] = null;
            Offices_model off = (Offices_model)Session["office"];
            var item = db.Get_TotalCitasByStatusxConsultories(off.uId).ToList();
            ViewBag.item = db.GetInvoicesByOfficeConsultory(off.uId).OrderByDescending(o => o.cita_date).ToList().Take(50);
            ViewBag.name = off.name;
            ViewBag.statics = db.GetCountCitasByConsultory(off.uId).ToList();
            ViewBag.month = db.GetMonthCitas(off.uId).ToList();
            ViewBag.status = db.GetCitasByStatus(off.uId).ToList();
            var list = db.GetGananciaByMonth(off.uId).ToList();
            ViewBag.datapie = list;
            return View(item);
        }

        [HttpPost]
        public ActionResult Citas(Guid uId)
        {
            //Session["consultory_uid"] = uId;
            //Consultory dep = db.Consultory.Find(uId);
            //ViewBag.duration = dep.time_prom;
            //var res = db.GetCitasByConsultory(uId).Select(x => new
            //{
            //    id = x.id,
            //    title = x.title,
            //    start = x.start.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
            //    end = x.end.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
            //    color = x.color,
            //    durationEditable = false,
            //    x.status_id
            //}).ToList();
            //ViewBag.item = res;
            //ViewBag.name = dep.name;
            //var dp = db.GetScheduleConsultory(dep.uId).ToList();
            //var model = db.GetInvoicesCitasByConsultory(dep.uId).ToList();
            //ViewBag.start = dp.OrderBy(x => x.start).FirstOrDefault().start;
            //ViewBag.ended = dp.OrderByDescending(x => x.ended).FirstOrDefault().ended;
            //ViewBag.uId = dep.uId;
            //ViewBag.ncitas = citasRepo.GetCitasOfToday(dep.uId).FirstOrDefault();
            //ViewBag.month = citasRepo.GetCitasOfTheMonth(dep.uId).FirstOrDefault();
            //var busss = db.GetConsultoryHoursByDoctor(dep.uId).ToList();
            //ViewBag.bussines = busss;
            return View();

        }

        public ActionResult Estadisticas()
        {
            Guid uId = Guid.Parse(Session["consultory_uid"].ToString());
            Consultory dep = db.Consultory.Find(uId);
            var model = db.GetLastCitas(dep.uId).ToList();
            try
            {
                //informacion adicional
                ViewBag.total = db.Citas.Where(x => x.consultory_uId == dep.uId).Count();

                //Bussines hours
                //var busss = db.GetConsultoryHoursByDoctor(dep.uId).ToList();
                //ViewBag.bussines = busss;

                //line
                var list = db.SPE_GananciaByMonthConsultory(dep.uId).ToList();
                ViewBag.datapie = list;
                ViewBag.monthd = db.GetMonthCitasByConsultory(dep.uId).ToList();
                //new datapie
                ViewBag.pay = db.SPE_GananciaXMes(dep.uId).ToList();
                //Status total citas
                ViewBag.status = db.GetCitasByStatusByConsultory(uId).ToList();

                //List 30 dias atras
                object[] treinta = new object[30];
                DateTime[] last30Days = Enumerable.Range(1, 30)
                .Select(i => DateTime.Now.Date.AddDays(-i))
                .ToArray();
                Boolean bandera1 = false;
                var contador1 = 0;
                var chart_30 = db.SPE_LAST30DAYS(uId).ToList();

                foreach (var item in last30Days)
                {
                    var format = item.ToString("yyyy-MM-dd");
                    foreach (var mes in chart_30)
                    {
                        var user_time = DateTime.Parse(format);
                        if (user_time == mes.fecha)
                        {
                            treinta[contador1] = new { mes = user_time, agendadas = mes.Agendadas, completadas = mes.Completadas, canceladas = mes.Canceladas };
                            Console.WriteLine("son iguales");
                            bandera1 = true;
                        }
                        else
                        {
                            if (bandera1 == false)
                            {
                                treinta[contador1] = new { mes = user_time, agendadas = 0, completadas = 0, canceladas = 0 };
                                Console.WriteLine("No son iguales");
                                bandera1 = true;
                            }
                        }
                    }
                    bandera1 = false;
                    contador1 = contador1 + 1;

                }
                Console.WriteLine(treinta);
                ViewBag.treinta = treinta;
                //Chart citas this month
                var lastSixMonths = Enumerable.Range(1, 6).Select(i => DateTime.Now.AddMonths(i - 6).Month);
                var chart = db.SPE_LASTSIXMONTHS(uId).ToList();
                ViewBag.chart = db.SPR_GetLastDataThisMonth(uId).ToList();
                object[] last = new object[6];
                Boolean bandera = false;
                var contador = 0;
                foreach (var item in lastSixMonths)
                {
                    foreach (var mes in chart)
                    {
                        if (item == mes.MES)
                        {
                            last[contador] = new { mes = item, agendadas = mes.Agendadas, completadas = mes.Completadas, canceladas = mes.Canceladas };
                            Console.WriteLine("son iguales");
                            bandera = true;
                        }
                        else
                        {
                            if (bandera == false)
                            {
                                last[contador] = new { mes = item, agendadas = 0, completadas = 0, canceladas = 0 };
                                Console.WriteLine("No son iguales");
                                bandera = true;
                            }
                        }
                    }
                    bandera = false;
                    contador = contador + 1;

                }
                ViewBag.last = last;
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("Estadisticas Error:" + ex.ToString());
            }
            return View(model);
        }

        public ActionResult Reportes()
        {
            return View();
        }


        public JsonResult GetPersons()
        {
            var item = db.Paciente.Select(x => new
            {
                id = x.id,
                nombre = x.nombre,
                apellidos = x.apellidos
            }).ToList();
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTable()
        {
            var item = db.Paciente.Select(x => new
            {
                id = x.id,
                nombre = x.nombre,
                apellidos = x.apellidos
            }).ToList();
            return Json(item, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Reports(Report response)
        {
            response.uId = Guid.Parse(Session["consultory_uid"].ToString());
            var item = repoReport.reports(response);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}
