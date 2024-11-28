using VLCitas.DataLayer.ConsultoriesRepository;
using DataTables;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using VLCitas.DataLayer.UsersRepository;
using VLCitas.DataLayer.OfficesRepository;
using VLCitas.DataLayer;
using VLCitas.DataLayer.Models;

namespace VLCitas.Controllers
{
    public class AdministradorController : MyController
    {
        // GET: Administrador
        public ActionResult Catalogos()
        {
            return View();
        }

        public ActionResult Doctores()
        {
            return View();
        }

        public ActionResult Consultorios()
        {
            return View();
        }


        public JsonResult ManageDoctors()
        {
            try
            {
                string dbConncection = ConfigurationManager.AppSettings["dbConnection"];
                Users user = (Users)Session["user"];
                var db = new DataTables.Database("sqlserver", dbConncection);
                DtResponse response = new Editor(db, "Users", "uId")
                    .Model<User_Model>("Users")
                    .Model<OfficeModel>("Offices")
                    .MJoin(new MJoin("Offices")
                    .Model<OfficeModel>()
                            .Link("Users.uId", "Offices_Users.user_uid")
                            .Link("Offices.uId", "Offices_Users.office_uid")
                            .Field(
                            new Field("uId")
                                .Options(new Options()
                            .Table("Offices")
                            .Value("uId")
                            .Label("name")
                            .Where(q =>
                            q.Where("uId", user.Offices_Users.FirstOrDefault().office_uid.ToString(), "=")
                            )
                        )
                            )
                        )
                      .MJoin(new MJoin("Roles")
                      .Model<RolesModel>()
                            .Link("Users.uId", "Roles_Users.Users_uId")
                            .Link("Roles.id", "Roles_Users.Roles_id")
                            .Field(
                            new Field("id")
                                .Options(new Options()
                            .Table("Roles")
                            .Value("id")
                            .Label("name")
                            .Where(q =>
                            q.Where("id", "3019", "=")
                            )
                        )
                            )
                        )
                        //.Where(q=> 
                        //q.Where(user.Offices_Users.FirstOrDefault().office_uid.ToString(), "select office_uid from Office_Users where user_uid = "+user.uId.ToString(), "IN", true)
                        //)
                        .Where("Offices.uId", user.Offices_Users.FirstOrDefault().office_uid.ToString(), "=")
                    .Where("Users.doctor_id", 0, ">")
                    .Debug(true)
                    .Process(Request.Form)
                    .Data();
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return Json(null);
            }
        }

        public JsonResult ManageConsultorios()
        {
            try
            {
                string dbConncection = ConfigurationManager.AppSettings["dbConnection"];
                Users user = (Users)Session["user"];
                var db = new DataTables.Database("sqlserver", dbConncection);
                DtResponse response = new Editor(db, "Consultory", "uId")
                    .Model<Consultory_Model>("Consultory")
                    .Model<OfficeModel>("Offices")
                    .Field(new Field("Consultory.office_uId")
                        .Options(new Options()
                            .Table("Offices")
                            .Value("uId")
                            .Label("name")
                            .Where(q =>
                            q.Where("uId", user.Offices_Users.FirstOrDefault().office_uid, "=")
                            )
                        )
                    )
                    .Field(new Field("Consultory.status_id")
                        .Options(new Options()
                            .Table("Consultory_Status")
                            .Value("id")
                            .Label("name")
                        )
                    )
                    .LeftJoin("Offices", "Offices.uId", "=", "Consultory.office_uId")
                    .Where("Consultory.office_uId", user.Offices_Users.FirstOrDefault().office_uid, "=")
                    .Debug(true)
                    .Process(Request.Form)
                    .Data();
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return Json(null);
            }
        }

    }
}