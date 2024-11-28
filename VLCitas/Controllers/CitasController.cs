using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLCitas.filters;
using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.DoctorsRepository;
using VLCitas.DataLayer;
using VLCitas.DataLayer.CitasRepository;
using VLCitas.DataLayer.OfficesRepository;
using VLCitas.DataLayer.ServicesRepository;
using VLCitas.DataLayer.Models;

namespace VLCitas.Controllers
{
    public class CitasController : MyController
    {
        // GET: Citas
        private VL_CitasEntities db;
        private CitaRepository citasRepo;
        private ServiceRepository serviceRepo;
        public CitasController()
        {
            db = new VL_CitasEntities();
            citasRepo = new CitaRepository();
            serviceRepo = new ServiceRepository();
        }

        [GeneralAcces]
        public ActionResult Index()
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

        [DoctorFilter]
        [HttpGet]
        public ActionResult Detalle(Guid cita_uid)
        {
            VLCitas.DataLayer.Citas cita = citasRepo.GetCita(cita_uid);
            ViewBag.id_paciente = cita.id_paciente;
            Users user = (Users)Session["user"];
            ViewBag.HasMoreOffices = user.Offices_Users.Count > 1 ? true : false;
            ViewBag.servicios = serviceRepo.GetAvailableServicesByUser(user.uId, cita_uid);
            ViewBag.last_cita = citasRepo.getLastCita(cita.id_paciente, cita.doctor_uid);
            Session["cita_uid"] = cita_uid;
            return View(cita);
        }

        [GeneralAcces]
        public ActionResult Consultorios()
        {
            bool selected = (bool)Session["selected_office"];
            if (selected)
            {
                Users user = (Users)Session["user"];
                return View(user);
            }
            else
            {
                return RedirectToAction("Offices", "Login");
            }
        }

        public ActionResult GoCalendar(Guid consultory_id)
        {
            return RedirectToAction("Consultorios", "Citas");
        }

        [GeneralAcces]
        [HttpPost]
        public ActionResult Agenda(Guid uId)
        {
            VL_CitasEntities db = new VL_CitasEntities();
            Users user = (Users)Session["user"];
            var consultory = db.Consultory.Where(x => x.uId == uId).FirstOrDefault();
            var doctor = consultory.Users.FirstOrDefault();
            Offices_model office = (Offices_model)Session["office"];
            List<CitasByDoctor> citas = citasRepo.GetCitasByConsultory(consultory.uId, HttpContext.Request.PathInfo, office.uId);
            ViewBag.citas = citas;
            ViewBag.name = office.name +" - "+ consultory.name;
            List<GetScheduleByConsultory_Result> schedules = db.GetScheduleByConsultory(consultory.uId, office.uId).ToList();
            ViewBag.schedules = schedules;
            TimeSpan? start_hour = schedules.OrderBy(x => x.start_hour).Select(x => x.start_hour).FirstOrDefault();
            TimeSpan? end_hour = schedules.OrderByDescending(x => x.end_hour).Select(x => x.end_hour).FirstOrDefault();
            ViewBag.start = start_hour == null ? new TimeSpan(0, 0, 0) : start_hour;
            ViewBag.ended = end_hour == null ? new TimeSpan(23, 59, 59) : end_hour;
            ViewBag.consultory_uId = uId;

            Offices_Users UserOfficeConfig = db.Offices_Users.Where(x => x.office_uid == office.uId && x.user_uid == doctor.uId).FirstOrDefault();
            ViewBag.additinalInfo = new AdditinalInfo(citas, UserOfficeConfig, office.configuration.Now);
            IEnumerable<Users> doctors = consultory.Users.Where(x => x.doctor_id != null);
            ViewBag.ListOfItems = new ListOfItems("Citas", "Doctor", doctors, citas);

            return View();
        }

        public ActionResult Status(Guid uid)
        {
            return View(citasRepo.GetCita(uid));
        }

        public JsonResult Confirm(Guid uId)
        {
            try
            {
                Offices_model office = (Offices_model)Session["office"];
                Response res = citasRepo.Confirm(uId, office);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("ConfirmarCita Error:" + ex.ToString());
                return null;
            }
        }

        public JsonResult ReloadEvents(Guid uId)
        {
            try
            {
                Offices_model office = (Offices_model)Session["office"];
                List<CitasByDoctor> citas = citasRepo.GetCitasByConsultory(uId, HttpContext.Request.PathInfo, office.uId);
                return Json(citas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("ReloadEvents Error:" + ex.ToString());
                return null;
            }
        }

        [GeneralAcces]
        public ActionResult AddCita(Cita_Model appointment)
        {
            try
            {
                Offices_model office = (Offices_model)Session["office"];
                Response item = citasRepo.Add(appointment, office);
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("AddCita Error:" + ex.ToString());
                return null;
            }
        }

        [GeneralAcces]
        public ActionResult Cancel(Cita_Model appointment)
        {
            try
            {
                Offices_model office = (Offices_model)Session["office"];
                Response item = citasRepo.Cancel(appointment, office.configuration.Now);
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("DeleteCita Error:" + ex.ToString());
                return null;
            }
        }

        [GeneralAcces]
        public JsonResult CompleteCita(Cita_Model appointment)
        {
            try
            {
                Offices_model office = (Offices_model)Session["office"];
                Response item = citasRepo.Complete(appointment, office.configuration.Now);
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("CompleteCita Error:" + ex.ToString());
                return null;
            }
        }

        [DoctorFilter]
        public JsonResult AddMedicine(Medicine_citas medicine)
        {
            try
            {
                Offices_model office = (Offices_model)Session["office"];
                Response item = citasRepo.AddMedicine(medicine);
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("CompleteCita Error:" + ex.ToString());
                return null;
            }
        }

        [DoctorFilter]
        public JsonResult RemoveMedicine(int id)
        {
            try
            {
                Offices_model office = (Offices_model)Session["office"];
                Response item = citasRepo.RemoveMedicine(id);
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("CompleteCita Error:" + ex.ToString());
                return null;
            }
        }


        [GeneralAcces]
        public JsonResult DetailsCita(Guid uId)
        {
            Response item = citasRepo.GetInformation(uId);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [GeneralAcces]
        public ActionResult ReAgenda(Cita_Model appointment)
        {
            Offices_model office = (Offices_model)Session["office"];
            Response item = citasRepo.ReSchedule(appointment, office);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [GeneralAcces]
        public JsonResult GetCalendar(Guid uId, DateTime date)
        {
            Offices_model office = (Offices_model)Session["office"];
            var total = 1;//db.Schedule.SelectMany(sc => sc.Consultory).Where(x => x.uId == uId).Count();
            if (total <= 1)
            {
                var query = db.SP_Horario_By_Doctor_Office(office.uId, uId, date).Where(x => x.OrderCount == 0).Select(x => new { hora = x.MyHour.Value.Hours, minutos = x.MyHour.Value.Minutes }).ToList();
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var query = db.SP_Horario_DisponibleByDoctor(office.uId, uId, date).Where(x => x.OrderCount == 0).Select(x => new { hora = x.MyHour.Value.Hours, minutos = x.MyHour.Value.Minutes }).ToList();
                return Json(query, JsonRequestBehavior.AllowGet);
            }
        }

        [GeneralAcces]
        public JsonResult GetPacientes(Guid doctor_uId)
        {
            DoctorRepository repo = new DoctorRepository();
            return Json(repo.GetPatients(doctor_uId), JsonRequestBehavior.AllowGet);
        }

        [GeneralAcces]
        public JsonResult SearchPatients(string data)
        {
            DoctorRepository repo = new DoctorRepository();
            return Json(repo.SearchPatients(data), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }


        public JsonResult SetDataPaciente(int id, int action, string data)
        {
            var item = citasRepo.SetExtraDataPaciente(id, action, data);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [GeneralAcces]
        public ActionResult SaveAttachments()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                if (Session["cita_uid"]!=null)
                {
                    Guid cita_uid = (Guid)Session["cita_uid"];
                    foreach (string fileName in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[fileName];
                        fName = file.FileName;
                        if (file != null && file.ContentLength > 0)
                        {
                            var originalDirectory = new DirectoryInfo(string.Format("{0}img\\patients", Server.MapPath(@"\")));
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");
                            var fileName1 = Path.GetFileName(file.FileName);
                            bool isExists = System.IO.Directory.Exists(pathString);
                            if (!isExists)
                                Directory.CreateDirectory(pathString);
                            var path = string.Format("{0}\\{1}", pathString, file.FileName);
                            file.SaveAs(path);
                        }
                    }
                }
                else
                    isSavedSuccessfully = false;
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("SaveAttachments Error:" + ex.ToString());
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
                return Json(new { Message = fName });
            else
                return Json(new { Message = "Error in saving file" });
        }
    }
}