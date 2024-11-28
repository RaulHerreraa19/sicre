using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VLCitas.filters
{
    public class OperadorFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] != null)
            {
                var controler_name = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var rol_id = Convert.ToInt32(filterContext.HttpContext.Session["rol"]);
                //user can enter
                switch (rol_id)
                {
                    
                    case 3019:
                    case 3021:
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
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Error",
                        action = "Unauthorised"
                    })
                );
                //filterContext.Result = new RedirectToRouteResult(
                //    new RouteValueDictionary
                //    {
                //    { "controller", "Home" },
                //    { "action", "Index" }
                //    });
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