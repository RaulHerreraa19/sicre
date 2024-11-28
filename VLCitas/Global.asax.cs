using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Web.SessionState;
using System.Net;
using VLCitas.DataLayer.CommonRepository;
using System.Security.Authentication;

namespace VLCitas
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Carga de la sesión
        /// </summary>
        /// <param name="sender">Objeto que lo invoca</param>
        /// <param name="e">Argumento de evento</param>
        protected void Session_Start(object sender, EventArgs e)
        {
            //Creación de instancia global
        }

        protected void Session_End(object sender, EventArgs E)
        {
            // Clean up session resources
            //var httpContext = ((MvcApplication)sender).Context;
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative);
        }

        protected void Application_EndRequest()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNetMvc-Version");
            Response.Headers.Remove("X-AspNet-Version");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            if (IsWebApiRequest()) {
                var app = (HttpApplication)sender;
                var req = app.Context.Request;
                if (!req.Url.ToString().Contains("/api/SAuth/Authenticate") && req.Url.ToString() != Settings.Url)
                {
                    if (!req.Headers.AllKeys.Contains("Authorization"))
                    {
                        Common.Set_Log_Errors("Application_BeginRequest || UnAuthorized Request >> Method:" + req.HttpMethod + " Url: " + req.Url.ToString() + " Headers: " + req.Headers.AllKeys);
                        //Context.Response.StatusCode = 401;
                        //Context.Response.End();
                    }
                }
            }
        }

        ///// <summary>
        ///// Función genérica para capturar errores no controlados
        ///// </summary>
        ///// <param name="sender">Objeto que generó la excepción</param>
        ///// <param name="e">evento</param>
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    System.Diagnostics.Trace.WriteLine("Enter - Application_Error");

        //    var httpContext = ((MvcApplication)sender).Context;

        //    var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));
        //    var currentController = " ";
        //    var currentAction = " ";

        //    if (currentRouteData != null)
        //    {
        //        if (currentRouteData.Values["controller"] != null && !string.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
        //            currentController = currentRouteData.Values["controller"].ToString();

        //        if (currentRouteData.Values["action"] != null && !string.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
        //            currentAction = currentRouteData.Values["action"].ToString();
        //    }
        //    var ex = Server.GetLastError();

        //    Server.ClearError();
        //    //Log.Write("[Error] GlobalAsax >>> Controller: " + currentController + " || Action: " + currentAction + " || Exception: " + ex != null ? ex.ToString() : "No se Identifico el Error");
        //    if (ex != null)
        //    {
        //        System.Diagnostics.Trace.WriteLine(ex.Message);

        //        if (ex.InnerException != null)
        //        {
        //            System.Diagnostics.Trace.WriteLine(ex.InnerException);
        //            System.Diagnostics.Trace.WriteLine(ex.InnerException.Message);
        //        }
        //    }

        //    var controller = new Controllers.ErrorController();
        //    var routeData = new RouteData();
        //    var statusCode = 406;

        //    if (ex is HttpException)
        //    {
        //        var httpEx = ex as HttpException;
        //        statusCode = httpEx.GetHttpCode();
        //    }
        //    else if (ex is AuthenticationException)
        //        statusCode = 403;
        //    if (ex.ToString().Contains("IController"))
        //        statusCode = 404;

        //    httpContext.ClearError();
        //    httpContext.Response.Clear();
        //    httpContext.Response.StatusCode = statusCode;
        //    //httpContext.Response.AppendHeader("Content-Type", "application/json");
        //    httpContext.Response.TrySkipIisCustomErrors = true;
        //    routeData.Values["controller"] = "Error";
        //    routeData.Values["action"] = "Generic";

        //    //wsLogClass.exceptionHandlerCatch.registerLogException(ex);

        //    controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
        //    ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        //    Response.End();
        //}
    }
}
