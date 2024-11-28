using VLCitas.filters;
using VLCitas.Models;
using VLCitas.DataLayer.CommonRepository;
using DataTables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLCitas.DataLayer.ServicesRepository;
using VLCitas.DataLayer;
using VLCitas.DataLayer.Models;
using VLCitas.DataLayer.UsersRepository;

namespace VLCitas.Controllers
{
    public class ServiciosController : MyController
    {
        private ServiceRepository serviceRepo;
        public ServiciosController()
        {
            serviceRepo = new ServiceRepository();
        }
        // GET: Servicios
        [DoctorFilter]
        public ActionResult Index()
        {
            //Guid dep_uid = Guid.Parse(Session["consultory_uid"].ToString());
            Users user = (Users)Session["user"];
            var model = serviceRepo.GetUser(user.uId);
            return View(model);
        }

        public JsonResult ManageServices()
        {
            try
            {
                string dbConncection = ConfigurationManager.AppSettings["dbConnection"];
                Users user = (Users)Session["user"];
                var db = new DataTables.Database("sqlserver", dbConncection);
                DtResponse response = new Editor(db, "Servicios", "id")
                    .Model<ServicesModel>("Servicios")
                    .Model<User_Model>("Users")
                    .Field(new Field("Servicios.user_uId")
                        .Options(new Options()
                            .Table("Users")
                            .Value("uId")
                            .Label("first_name")
                            .Where(q =>
                            q.Where("uid", user.uId, "=")
                            )
                        )
                    )
                    .LeftJoin("Users", "Users.uId", "=", "Servicios.user_uId")
                    .Where("Servicios.user_uId", user.uId, "=")
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

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int service_id)
        {
            var model = serviceRepo.GetService(service_id);
            return View(model);
        }

        [HttpPost]
        public JsonResult AddService(ServicioModel servicio)
        {
            Users user = (Users)Session["user"];
            var res = servicio.Save(user.uId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateService(ServicioModel servicio)
        {
            var res = servicio.Update();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(ServicioModel servicio)
        {
            var res = servicio.Delete();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetServicioToCita(CitaServicio servicio)
        {
            var res = servicio.SetServicioToCita();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}