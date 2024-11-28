using System;
using System.Globalization;
using System.Threading;
using System.Web;
using VLCitas.DataLayer;

namespace VLCitas
{
    public class LanguageMang : LanguageManager
    {
        public HttpCookie SetLanguage(Languages language)
        {
            try
            {
                CultureInfo cultureInfo = new CultureInfo(language.LanguageCultureName);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                HttpCookie langCookie = new HttpCookie("culture", language.LanguageCultureName)
                {
                    Expires = DateTime.Now.AddYears(1)
                };
                HttpContext.Current.Response.Cookies.Add(langCookie);
                return langCookie;
            }
            catch (Exception) { }
            return null;
        }

        public HttpCookie SetLanguage(string lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang))
                    lang = GetDefaultLanguage();
                string lng = lang == "esMX" ? "es-MX" : lang;
                CultureInfo cultureInfo = new CultureInfo(lng);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                HttpCookie langCookie = new HttpCookie("culture", lang)
                {
                    Expires = DateTime.Now.AddYears(1)
                };
                HttpContext.Current.Response.Cookies.Add(langCookie);
                return langCookie;
            }
            catch (Exception) { }
            return null;
        }

       
    }


}