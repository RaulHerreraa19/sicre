using VLCitas.DataLayer.Models;
using VLCitas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VLCitas.filters
{
    public class DoctorFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] != null && filterContext.HttpContext.Session["rol"] != null)
            {
                var controler_name = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var rol_id = Convert.ToInt32(filterContext.HttpContext.Session["rol"]);
                UserRoles rol = (UserRoles)rol_id;
                switch (rol)
                {
                    case UserRoles.Doctor:
                        break;
                    default:
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new
                                {
                                    controller = "Error",
                                    action = "Unauthorised"
                                })
                            );
                        break;
                }
               
            }
            if (filterContext.HttpContext.Session["user"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                                 new RouteValueDictionary(
                                     new
                                     {
                                         controller = "Login",
                                         action = "Index"
                                     })
                                 );
            }
        }

    }
}