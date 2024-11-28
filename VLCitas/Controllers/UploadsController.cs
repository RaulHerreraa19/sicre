using VLCitas.Models;
using VLCitas.DataLayer.CommonRepository;
using DataTables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Http;
using VLCitas.DataLayer;
using VLCitas.DataLayer.UsersRepository;

namespace VLCitas.Controllers
{
    public class UploadsController : ApiController
    {
        [HttpGet, HttpPost]
        public IHttpActionResult ManageProfile()
        {
            try
            {
                string dbConncection = ConfigurationManager.AppSettings["dbConnection"];
                Users user = (Users)HttpContext.Current.Session["user"];
                var request = HttpContext.Current.Request;
                var path = HttpContext.Current.Server.MapPath("~/uploads");
                //var db = new DataTables.Database("sqlserver", dbConncection);
                using (var db = new DataTables.Database("sqlserver", dbConncection))
                {
                    DtResponse response = new Editor(db, "Doctor_Configs", "id")
                        .Model<DoctorConfigModel>("Doctor_Configs")
                        .Model<User_Model>("Users")
                        .LeftJoin("Users", "Users.doctor_id", "=", "Doctor_Configs.id")
                        .Where("Users.uId", user.uId, "=")
                        .MJoin(new MJoin("Image")
                            .Link("Doctor_Configs.id", "Configs_Images.config_id")
                            .Link("Image.id", "Configs_Images.image_id")
                            .Field(
                            new Field("id")
                                .Upload(
                               new Upload(request.PhysicalApplicationPath + @"uploads\__ID____EXTN__")
                                    .Db("Image", "id", new Dictionary<string, object>
                                    {
                                    {"fileName", Upload.DbType.FileName},
                                    {"webPath", Upload.DbType.WebPath},
                                    {"systemPath", Upload.DbType.SystemPath},
                                    })
                                    .Validator(Validation.FileSize(500000, "Max file size is 500K."))
                                    .Validator(Validation.FileExtensions(new[] { "jpg", "png", "gif" }, "Please upload an image."))
                                    )
                            )
                        )

                        .Debug(true)
                        .Process(request)
                        .Data();

                    return Json(response);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                Common.Set_Log_Errors("ManageProfile - Error: " + ex.ToString());
                return Json("");
            }
        }

    }
}
