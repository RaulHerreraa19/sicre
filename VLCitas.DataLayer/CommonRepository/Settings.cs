using System;
using System.Configuration;

namespace VLCitas.DataLayer.CommonRepository
{
    public class Settings
    {
        private static AppSettingsReader settingsReader = new AppSettingsReader();
        public static string SecurityKey
        {
            get
            {
                return (string)settingsReader.GetValue("SecurityKey", typeof(String));
            }
        }

        public static string ServerPath
        {
            get
            {
                return (string)settingsReader.GetValue("url", typeof(String));
            }
        }

        public static string Email
        {
            get
            {
                return (string)settingsReader.GetValue("email", typeof(String));
            }
        }

        public static string Password
        {
            get
            {
                return (string)settingsReader.GetValue("password", typeof(String));
            }
        }

        public static string Url
        {
            get
            {
                return (string)settingsReader.GetValue("url", typeof(String));
            }
        }

        public static Languages Language { get;  set; }
    }
}