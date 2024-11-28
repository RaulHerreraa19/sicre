using System.Collections.Generic;
using System.Linq;

namespace VLCitas.DataLayer
{
    public class Languages
    {
        public Languages() { }
        public Languages(int l)
        {
            ParseModel(LanguageManager.AvailableLanguages[l]);
        }
        private void ParseModel(Languages Model)
        {
            LanguageFullName = Model.LanguageFullName;
            LanguageCultureName = Model.LanguageCultureName;
            JavascriptName = Model.JavascriptName;
            DateFormat = Model.DateFormat;
            LanguageCulture = Model.LanguageCulture;
            Currency = Model.Currency;
        }
        public string LanguageFullName { get; set; }
        public string LanguageCultureName { get; set; }
        public string JavascriptName { get; set; }
        public string DateFormat { get; set; }
        public string LanguageCulture { get; set; }
        public string Currency { get; set; }
    }

    public class LanguageManager
    {
        public static List<Languages> AvailableLanguages = new List<Languages> {
            new Languages {
                LanguageFullName = "English", LanguageCultureName = "en", JavascriptName = "en", DateFormat = "MM/dd/yyyy", LanguageCulture = "en",Currency = "USD"
            },
            new Languages {
                LanguageFullName = "Spanish (Mexico)", LanguageCultureName = "es-MX", JavascriptName = "esMX", DateFormat = "dd/MM/yyyy", LanguageCulture = "es",Currency = "MXN"
            },
            new Languages {
                LanguageFullName = "japanese", LanguageCultureName = "Ja", JavascriptName = "Ja", DateFormat = "MM/dd/yyyy",LanguageCulture = "ja",Currency = "USD"
            },
            new Languages {
                LanguageFullName = "arabic", LanguageCultureName = "Ar", JavascriptName = "Ar", DateFormat = "MM/dd/yyyy",LanguageCulture = "ar",Currency = "USD"
            },
        };
        public static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.Where(a => a.LanguageCultureName.Equals(lang)).FirstOrDefault() != null ? true : false;
        }
        public static string GetDefaultLanguage()
        {
            return AvailableLanguages[0].LanguageCultureName;
        }

        public static Languages GetLanguage(string lang)
        {
            Languages language = AvailableLanguages.Where(a => a.LanguageCultureName.Equals(lang)).FirstOrDefault();
            if (language != null)
                return language;
            else
                return AvailableLanguages[0];
        }
    }


}