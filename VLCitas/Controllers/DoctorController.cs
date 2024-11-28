using VLCitas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataTables;
using System.Configuration;
using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.DoctorsRepository;
using VLCitas.filters;
using VLCitas.DataLayer;
using VLCitas.DataLayer.OfficesRepository;
using VLCitas.DataLayer.UsersRepository;
using VLCitas.DataLayer.Models;
using VLCitas.DataLayer.CitasRepository;

namespace VLCitas.Controllers
{
    public class DoctorController : MyController
    {
        private DoctorRepository drRepo;
        private CitaRepository citasRepo;
        public DoctorController()
        {
            drRepo = new DoctorRepository();
            citasRepo = new CitaRepository();
        }

        [DoctorFilter]
        public ActionResult Calendario()
        {
            bool selected = (bool)Session["selected_office"];
            if (selected)
            {
                VL_CitasEntities db = new VL_CitasEntities();
                Users user = (Users)Session["user"];
                Offices_model office = (Offices_model)Session["office"];
                if (user.Consultory.Where(x=>x.office_uId == office.uId).Count() == 1)
                    return RedirectToAction("Consultorio", "Doctor", new { uId = user.Consultory.Where(x => x.office_uId == office.uId).Select(x=>x.uId).FirstOrDefault() });
                else
                {
                    List<CitasByDoctor> citas = citasRepo.GetCitas(user.uId, HttpContext.Request.PathInfo);
                    ViewBag.citas = citas;
                    ViewBag.name = user.first_name + " " + user.last_name;
                    List<GetScheduleByOfficeAndDoctor_Result> schedules = db.GetScheduleByOfficeAndDoctor(user.uId).ToList();
                    ViewBag.schedules = schedules;
                    TimeSpan? start_hour = schedules.OrderBy(x => x.start_hour).Select(x => x.start_hour).FirstOrDefault();
                    TimeSpan? end_hour = schedules.OrderByDescending(x => x.end_hour).Select(x => x.end_hour).FirstOrDefault();
                    ViewBag.start = start_hour == null ? new TimeSpan(0, 0, 0) : start_hour;
                    ViewBag.ended = end_hour == null ? new TimeSpan(23, 59, 59) : end_hour;
                    ViewBag.doctor_uId = user.uId;

                    Offices_Users UserOfficeConfig = db.Offices_Users.Where(x => x.office_uid == office.uId && x.user_uid == user.uId).FirstOrDefault();
                    ViewBag.additinalInfo = new AdditinalInfo(citas, UserOfficeConfig, office.configuration.Now);
                    ViewBag.HasMoreOffices = user.Offices_Users.Count > 1 ? true : false;
                    IEnumerable<Consultory> consultoriesByOffice = user.Consultory.Where(x => x.office_uId == office.uId && x.status_id == 1);
                    ViewBag.ListOfItems = new ListOfItems("Consultorio", "Doctor", consultoriesByOffice, citas);
                    return View();
                }
                
            }
            else
            {
                return RedirectToAction("Offices", "Login");
            }
        }

        [DoctorFilter]
        public ActionResult Consultorio(Guid uId)
        {
            bool selected = (bool)Session["selected_office"];
            if (selected)
            {
                VL_CitasEntities db = new VL_CitasEntities();
                Users user = (Users)Session["user"];
                Offices_model office = (Offices_model)Session["office"];
                ViewBag.consultory_name = user.Consultory.Where(x => x.uId == uId).Select(x=>x.name).FirstOrDefault();
                List<CitasByDoctor> citas = citasRepo.GetCitasByConsultory(uId, HttpContext.Request.PathInfo, office.uId);
                foreach (CitasByDoctor cita in citas.Where(x=>x.doctor_uid!= user.uId))
                {
                    cita.color = "#989898";
                    cita.url = "#";
                }
                ViewBag.citas = citas;
                ViewBag.name = user.first_name + " " + user.last_name;
                List<GetScheduleByOfficeAndDoctor_Result> schedules = db.GetScheduleByOfficeAndDoctor(user.uId).ToList();
                ViewBag.schedules = schedules;
                TimeSpan? start_hour = schedules.OrderBy(x => x.start_hour).Select(x => x.start_hour).FirstOrDefault();
                TimeSpan? end_hour = schedules.OrderByDescending(x => x.end_hour).Select(x => x.end_hour).FirstOrDefault();
                ViewBag.start = start_hour == null ? new TimeSpan(0, 0, 0) : start_hour;
                ViewBag.ended = end_hour == null ? new TimeSpan(23, 59, 59) : end_hour;
                ViewBag.doctor_uId = user.uId;

                Offices_Users UserOfficeConfig = db.Offices_Users.Where(x => x.office_uid == office.uId && x.user_uid == user.uId).FirstOrDefault();
                ViewBag.additinalInfo = new AdditinalInfo(citas, UserOfficeConfig, office.configuration.Now);
                ViewBag.HasMoreOffices = user.Offices_Users.Count > 1 ? true : false;
                IEnumerable<Consultory> consultoriesByOffice = user.Consultory.Where(x => x.office_uId == office.uId && x.status_id == 1);
                ViewBag.ListOfItems = new ListOfItems("Consultory", "Doctor", consultoriesByOffice, citas);
                return View();
            }
            else
            {
                return RedirectToAction("Offices", "Login");
            }
        }


        public JsonResult ReloadEvents(Guid uId)
        {
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
                var item = citasRepo.GetCitas(uId, HttpContext.Request.PathInfo);
                //var item = db.GetCitasByConsultory(uId).Select(x => new
                //{
                //    id = x.id,
                //    title = x.title,
                //    start = x.start.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                //    end = x.end.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                //    color = x.color,
                //    durationEditable = false,
                //    x.status_id,
                //    url = HttpContext.Request.PathInfo + "/Citas/Detalle?cita_uid=" + x.id
                //}).ToList();
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("ReloadEvents Error:" + ex.ToString());
                return null;
            }
        }

        // GET: Doctor
        public JsonResult ManageProfile()
        {
            try
            {
                string dbConncection = ConfigurationManager.AppSettings["dbConnection"];
                Users user = (Users)Session["user"];
                var db = new DataTables.Database("sqlserver", dbConncection);
                DtResponse response = new Editor(db, "Doctor_Configs", "id")
                    .Model<DoctorConfigModel>("Doctor_Configs")
                    .Model<User_Model>("Users")
                    .LeftJoin("Users", "Users.doctor_id", "=", "Doctor_Configs.id")
                    .Where("Users.uId", user.uId, "=")
                    .Process(Request.Form)
                    .Data();
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                Common.Set_Log_Errors("ManageProfile - Error: " + ex.ToString());
                return Json(null);
            }
        }


        public JsonResult ManageVacations()
        {
            try
            {
                string dbConncection = ConfigurationManager.AppSettings["dbConnection"];
                Users user = (Users)Session["user"];
                var db = new DataTables.Database("sqlserver", dbConncection);
                DtResponse response = new Editor(db, "Vacations", "id")
                    .Model<VacationsModel>("Vacations")
                    .Field(new Field("Vacations.user_uid")
                        .Options(new Options()
                            .Table("Users")
                            .Value("uId")
                            .Label("first_name")
                            .Where(q =>
                            q.Where("uid", user.uId, "=")
                            )
                        )
                    )
                    .LeftJoin("Users", "Users.uId", "=", "Vacations.user_uid")
                    .Where("Vacations.user_uid", user.uId, "=")
                    .Process(Request.Form)
                    .Data();
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                Common.Set_Log_Errors("ManageProfile - Error: " + ex.ToString());
                return Json(null);
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DoctorFilter]
        public ActionResult Perfil()
        {
            VL_CitasEntities db = new VL_CitasEntities();
            Users userSession = (Users)Session["user"];
            Users user = db.Users.Where(x => x.uId == userSession.uId).FirstOrDefault();
            ViewBag.Specialities = db.Specialties.OrderBy(x=> x.name).Select(x => new { x.id, x.name }).ToList();
            return View(user);
        }

        public JsonResult UpdateAboutYou(AboutYouModel model)
        {
            UserRepository user = new UserRepository();
            Response res = user.UpdateAboutYou(model);
            Session["user"] = (Users)res.Data;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateClinicInfo(ClinicInfoModel model)
        {
            UserRepository user = new UserRepository();
            Offices_model office = (Offices_model)Session["office"];
            model.office_uid = office.uId;
            Response res = user.UpdateClinicInfo(model);
            Session["user"] = (Users)res.Data;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateSpecialities(User_specialitiesModel model)
        {
            UserRepository user = new UserRepository();
            Response res = user.UpdateSpecialities(model);
            Session["user"] = (Users)res.Data;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [DoctorFilter]
        public ActionResult Vacaciones()
        {
            return View();
        }

        [DoctorFilter]
        public ActionResult Reportes()
        {
            VL_CitasEntities db = new VL_CitasEntities();
            UserRoles userRol = (UserRoles)Session["rol"];
            int rol_id = (int)userRol;
            Roles role = db.Roles.Find(rol_id);
            return View(role);
        }
    }
}