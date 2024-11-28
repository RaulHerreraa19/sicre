using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VLCitas.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Controlador de error genérico
        /// </summary>
        /// <returns></returns>
        //[DonutOutputCache(CacheProfile = "TwentyFourHours")]
        public ActionResult Generic()
        {
            return View();
        }

        /// <summary>
        /// Controlador de error de navegador sin soporte JavaScript
        /// </summary>
        /// <param name="refreshUri">Url a donde se redireccionará</param>
        /// <returns></returns>
        //[DonutOutputCache(CacheProfile = "TwentyFourHours")]
        public ActionResult noScript(string refreshUri = null)
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(refreshUri);
            ViewBag.refreshUri = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            //TODO: ANGEL => Sustituir mensajes
            wsLogClass.exceptionHandlerCatch.registerLogException(new Exception("Lo sentimos, su navegador no soporta JavaScript"));
            return View();
        }

        /// <summary>
        /// Controlador de error de navegador incompatible con HTML5
        /// </summary>
        /// <returns></returns>
        //[DonutOutputCache(CacheProfile = "TwentyFourHours")]
        public ActionResult incompatible()
        {
            //TODO: ANGEL => Sustituir mensajes
            wsLogClass.exceptionHandlerCatch.registerLogException(new Exception("Lo sentimos, la versión de su navegador no es compatible con los requerimientos mínimos del sitio. El sitio requiere un navegador que soporte HTML5"));
            return View();
        }

        /// <summary>
        /// Controlador de error de navegador incompatible con HTML5
        /// </summary>
        /// <returns></returns>
        //[DonutOutputCache(CacheProfile = "TwentyFourHours")]
        public ActionResult browserNotSupported()
        {
            //TODO: ANGEL => Sustituir mensajes
            wsLogClass.exceptionHandlerCatch.registerLogException(new Exception("Lo sentimos, el sitio no soporta su navegador"));
            return View();
        }

        public ActionResult NoAutorizado()
        {
            return View();
        }

        public ActionResult _Permiso()
        {
            return View();
        }
    }
}