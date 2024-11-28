using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLCitas.filters;
using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer;
using VLCitas.DataLayer.OfficesRepository;
using VLCitas.DataLayer.UsersRepository;
using VLCitas.DataLayer.Models;

namespace VLCitas.Controllers
{
    public class LoginController : MyController
    {
        private UserRepository userRepo;
        public LoginController()
        {
            userRepo = new UserRepository();
        }
        // GET: Login
        public ActionResult Index()
        {
            try
            {
                Languages lang = (Languages)Session["userLanguage"];
                if (lang == null)
                {
                    string culture;
                    var cookie = Request.Cookies["culture"];
                    if (cookie.Values[0].ToString().Contains("="))
                        culture = cookie.Values["language"].ToString();
                    else
                        culture = cookie.Values[0].ToString();
                    var res = ChangeLanguage(culture);
                }

                if (Session["login_view"]!=null)
                {
                    DataView data = (DataView)Session["login_view"];
                    return RedirectToAction(data.view_name, data.controller_name);
                }

            }
            catch { }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pwd)
        {
            try
            {
                Users User = userRepo.ValidateUser(user, pwd);
                bool errorLogin = true;
                DataView data = new DataView();
                if (User != null)
                {
                    int countOfficeUsers = User.Offices_Users.Count;
                    if (countOfficeUsers > 0)
                    {
                        int rol = 1;
                        if (countOfficeUsers == 1)
                        {
                            Session["office"] = new Offices_model(User.Offices_Users.FirstOrDefault().Offices);
                            Offices_Users office_user = User.Offices_Users.FirstOrDefault();
                            if (office_user.Roles.Count == 0)
                                rol = 0;
                            else if (office_user.Roles.Count == 1)
                            {
                                rol = office_user.Roles.Select(x => x.id).FirstOrDefault();
                                Session["Modules"] = office_user.Roles.FirstOrDefault().Module.ToList();
                                Session["selected_office"] = true;
                            }
                        }

                        UserRoles userRol = (UserRoles)rol;
                        Session["rol"] = userRol;
                        Session["user"] = User;
                        Common.Set_Log_Details("Login User:" + user + " Rol:" + userRol.ToString());
                        switch (userRol)
                        {
                            case UserRoles.MoreThanOneRol:
                                data.view_name = "Offices";
                                data.controller_name = "Login";
                                errorLogin = false;
                                break;
                            case UserRoles.Administrador:
                                data.view_name = "Catalogos";
                                data.controller_name = "Administrador";
                                errorLogin = false;
                                break;
                            case UserRoles.Assistant:
                                data.view_name = "Consultorios";
                                data.controller_name = "Citas";
                                errorLogin = false;
                                break;
                            case UserRoles.Doctor:
                                data.view_name = "Calendario";
                                data.controller_name = "Doctor";
                                errorLogin = false;
                                break;
                            default:
                                break;
                        }
                    }

                }
                else
                {
                    TempData["Message"] = Resource.LoginError;
                    return View("Index");
                }
                if (errorLogin)
                {
                    Session.Abandon();
                    TempData["Message"] = Resource.NoOfficeError;
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["login_view"] = data;
                    return RedirectToAction(data.view_name, data.controller_name);
                }
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("Login - Error: " + ex.ToString());
                return RedirectToAction("Index");
            }
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }

        [GeneralAcces]
        public ActionResult Offices()
        {
            Session["selected_office"] = false;
            Session["Modules"] = null;
            Users model = (Users)Session["user"];
            return View(model);
        }

        public ActionResult GoOffice(Guid office_uid, int rol_id)
        {
            VL_CitasEntities db = new VL_CitasEntities();
            Users user = (Users)Session["user"];
            Session["rol"] = (UserRoles)rol_id;
            Session["office"] = new Offices_model(user.Offices_Users.Where(x => x.office_uid == office_uid).Select(y => y.Offices).FirstOrDefault());
            Session["Modules"] = db.Roles.Find(rol_id).Module.ToList();
            Session["selected_office"] = true;
            UserRoles userRol = (UserRoles)Session["rol"];
            if (userRol == UserRoles.Doctor)
            {
                return RedirectToAction("Calendario", "Doctor");
            }
            else
            {
                return RedirectToAction("Consultorios", "Citas");
            }
        }

        public Response ChangeLanguage(string lang)
        {
            Response.Cookies.Remove("Language");
            LanguageMang Language = new LanguageMang();
            Languages language = LanguageMang.GetLanguage(lang);
            HttpCookie languageCookie = Language.SetLanguage(lang);
            Response res = new Response
            {
                TypeOfResponse = TypeOfResponse.OK,
                Message = "Success"
            };
            Session["userLanguage"] = language;
            Settings.Language = language;
            Response.SetCookie(languageCookie);
            return res;
        }

    }
}