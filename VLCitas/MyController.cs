using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VLCitas
{
    public class MyController : Controller
    {

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
                lang = langCookie.Value;
            else
            {
                string[] userLanguage = Request.UserLanguages;
                string userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                    lang = userLang;
                else
                    lang = VLCitas.DataLayer.LanguageManager.GetDefaultLanguage();
            }
            new LanguageMang().SetLanguage(lang);
            return base.BeginExecuteCore(callback, state);
        }

    }
}