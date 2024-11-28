using System;
using System.Collections.Generic;

namespace VLCitas.DataLayer.CommonRepository
{
    public class TimeZonesClass
    {
        public string Id { get; set; }
        public string DaylightName { get; set; }
        public string DisplayName { get; set; }
        public string StandardName { get; set; }
        public string BaseUtcOffset { get; set; }

        public TimeZonesClass(string Id, string DaylightName, string DisplayName, string StandardName, string BaseUtcOffset)
        {
            this.Id = Id;
            this.DaylightName = DaylightName;
            this.DisplayName = DisplayName;
            this.StandardName = StandardName;
            this.BaseUtcOffset = BaseUtcOffset;
        }

        public static DateTime GetDateByTimeZone(DateTime inputDateTime, string TimeZoneName)
        {
            DateTimeOffset userTime = DateTimeOffset.Now;
            try
            {
                DateTime utcTime = inputDateTime.ToUniversalTime();
                TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneName);
                DateTimeOffset localServerTime = DateTimeOffset.Now;
                userTime = TimeZoneInfo.ConvertTime(localServerTime, timeInfo);
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("GetDateByTimeZone " + ex.ToString());
            }
            return userTime.DateTime;
        }



        public static List<TimeZonesClass> GetTimeZones()
        {
            List<TimeZonesClass> Zones = new List<TimeZonesClass>
            {
                new TimeZonesClass("Dateline Standard Time", "Dateline Daylight Time", "(UTC-12:00) International Date Line West", "Dateline Standard Time", "-12:00:00"),
                new TimeZonesClass("UTC-11", "UTC-11", "(UTC-11:00) Coordinated Universal Time-11", "UTC-11", "-11:00:00"),
                new TimeZonesClass("Hawaiian Standard Time", "Hawaiian Daylight Time", "(UTC-10:00) Hawaii", "Hawaiian Standard Time", "-10:00:00"),
                new TimeZonesClass("Alaskan Standard Time", "Alaskan Daylight Time", "(UTC-09:00) Alaska", "Alaskan Standard Time", "-09:00:00"),
                new TimeZonesClass("Pacific Standard Time (Mexico)", "Pacific Daylight Time (Mexico)", "(UTC-08:00) Baja California", "Pacific Standard Time (Mexico)", "-08:00:00"),
                new TimeZonesClass("Pacific Standard Time", "Pacific Daylight Time", "(UTC-08:00) Pacific Time (US & Canada)", "Pacific Standard Time", "-08:00:00"),
                new TimeZonesClass("US Mountain Standard Time", "US Mountain Daylight Time", "(UTC-07:00) Arizona", "US Mountain Standard Time", "-07:00:00"),
                new TimeZonesClass("Mountain Standard Time (Mexico)", "Mountain Daylight Time (Mexico)", "(UTC-07:00) Chihuahua, La Paz, Mazatlan", "Mountain Standard Time (Mexico)", "-07:00:00"),
                new TimeZonesClass("Mountain Standard Time", "Mountain Daylight Time", "(UTC-07:00) Mountain Time (US & Canada)", "Mountain Standard Time", "-07:00:00"),
                new TimeZonesClass("Central America Standard Time", "Central America Daylight Time", "(UTC-06:00) Central America", "Central America Standard Time", "-06:00:00"),
                new TimeZonesClass("Central Standard Time", "Central Daylight Time", "(UTC-06:00) Central Time (US & Canada)", "Central Standard Time", "-06:00:00"),
                new TimeZonesClass("Central Standard Time (Mexico)", "Central Daylight Time (Mexico)", "(UTC-06:00) Guadalajara, Mexico City, Monterrey", "Central Standard Time (Mexico)", "-06:00:00"),
                new TimeZonesClass("Canada Central Standard Time", "Canada Central Daylight Time", "(UTC-06:00) Saskatchewan", "Canada Central Standard Time", "-06:00:00"),
                new TimeZonesClass("SA Pacific Standard Time", "SA Pacific Daylight Time", "(UTC-05:00) Bogota, Lima, Quito, Rio Branco", "SA Pacific Standard Time", "-05:00:00"),
                new TimeZonesClass("Eastern Standard Time (Mexico)", "Eastern Daylight Time (Mexico)", "(UTC-05:00) Chetumal", "Eastern Standard Time (Mexico)", "-05:00:00"),
                new TimeZonesClass("Eastern Standard Time", "Eastern Daylight Time", "(UTC-05:00) Eastern Time (US & Canada)", "Eastern Standard Time", "-05:00:00"),
                new TimeZonesClass("US Eastern Standard Time", "US Eastern Daylight Time", "(UTC-05:00) Indiana (East)", "US Eastern Standard Time", "-05:00:00"),
                new TimeZonesClass("Venezuela Standard Time", "Venezuela Daylight Time", "(UTC-04:30) Caracas", "Venezuela Standard Time", "-04:30:00"),
                new TimeZonesClass("Paraguay Standard Time", "Paraguay Daylight Time", "(UTC-04:00) Asuncion", "Paraguay Standard Time", "-04:00:00"),
                new TimeZonesClass("Atlantic Standard Time", "Atlantic Daylight Time", "(UTC-04:00) Atlantic Time (Canada)", "Atlantic Standard Time", "-04:00:00"),
                new TimeZonesClass("Central Brazilian Standard Time", "Central Brazilian Daylight Time", "(UTC-04:00) Cuiaba", "Central Brazilian Standard Time", "-04:00:00"),
                new TimeZonesClass("SA Western Standard Time", "SA Western Daylight Time", "(UTC-04:00) Georgetown, La Paz, Manaus, San Juan", "SA Western Standard Time", "-04:00:00"),
                new TimeZonesClass("Newfoundland Standard Time", "Newfoundland Daylight Time", "(UTC-03:30) Newfoundland", "Newfoundland Standard Time", "-03:30:00"),
                new TimeZonesClass("E. South America Standard Time", "E. South America Daylight Time", "(UTC-03:00) Brasilia", "E. South America Standard Time", "-03:00:00"),
                new TimeZonesClass("SA Eastern Standard Time", "SA Eastern Daylight Time", "(UTC-03:00) Cayenne, Fortaleza", "SA Eastern Standard Time", "-03:00:00"),
                new TimeZonesClass("Argentina Standard Time", "Argentina Daylight Time", "(UTC-03:00) City of Buenos Aires", "Argentina Standard Time", "-03:00:00"),
                new TimeZonesClass("Greenland Standard Time", "Greenland Daylight Time", "(UTC-03:00) Greenland", "Greenland Standard Time", "-03:00:00"),
                new TimeZonesClass("Montevideo Standard Time", "Montevideo Daylight Time", "(UTC-03:00) Montevideo", "Montevideo Standard Time", "-03:00:00"),
                new TimeZonesClass("Bahia Standard Time", "Bahia Daylight Time", "(UTC-03:00) Salvador", "Bahia Standard Time", "-03:00:00"),
                new TimeZonesClass("Pacific SA Standard Time", "Pacific SA Daylight Time", "(UTC-03:00) Santiago", "Pacific SA Standard Time", "-03:00:00"),
                new TimeZonesClass("UTC-02", "UTC-02", "(UTC-02:00) Coordinated Universal Time-02", "UTC-02", "-02:00:00"),
                new TimeZonesClass("Mid-Atlantic Standard Time", "Mid-Atlantic Daylight Time", "(UTC-02:00) Mid-Atlantic - Old", "Mid-Atlantic Standard Time", "-02:00:00"),
                new TimeZonesClass("Azores Standard Time", "Azores Daylight Time", "(UTC-01:00) Azores", "Azores Standard Time", "-01:00:00"),
                new TimeZonesClass("Cape Verde Standard Time", "Cabo Verde Daylight Time", "(UTC-01:00) Cabo Verde Is.", "Cabo Verde Standard Time", "-01:00:00"),
                new TimeZonesClass("Morocco Standard Time", "Morocco Daylight Time", "(UTC) Casablanca", "Morocco Standard Time", "00:00:00"),
                new TimeZonesClass("UTC", "Coordinated Universal Time", "(UTC) Coordinated Universal Time", "Coordinated Universal Time", "00:00:00"),
                new TimeZonesClass("GMT Standard Time", "GMT Daylight Time", "(UTC) Dublin, Edinburgh, Lisbon, London", "GMT Standard Time", "00:00:00"),
                new TimeZonesClass("Greenwich Standard Time", "Greenwich Daylight Time", "(UTC) Monrovia, Reykjavik", "Greenwich Standard Time", "00:00:00"),
                new TimeZonesClass("W. Europe Standard Time", "W. Europe Daylight Time", "(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna", "W. Europe Standard Time", "01:00:00"),
                new TimeZonesClass("Central Europe Standard Time", "Central Europe Daylight Time", "(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague", "Central Europe Standard Time", "01:00:00"),
                new TimeZonesClass("Romance Standard Time", "Romance Daylight Time", "(UTC+01:00) Brussels, Copenhagen, Madrid, Paris", "Romance Standard Time", "01:00:00"),
                new TimeZonesClass("Central European Standard Time", "Central European Daylight Time", "(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb", "Central European Standard Time", "01:00:00"),
                new TimeZonesClass("W. Central Africa Standard Time", "W. Central Africa Daylight Time", "(UTC+01:00) West Central Africa", "W. Central Africa Standard Time", "01:00:00"),
                new TimeZonesClass("Namibia Standard Time", "Namibia Daylight Time", "(UTC+01:00) Windhoek", "Namibia Standard Time", "01:00:00"),
                new TimeZonesClass("Jordan Standard Time", "Jordan Daylight Time", "(UTC+02:00) Amman", "Jordan Standard Time", "02:00:00"),
                new TimeZonesClass("GTB Standard Time", "GTB Daylight Time", "(UTC+02:00) Athens, Bucharest", "GTB Standard Time", "02:00:00"),
                new TimeZonesClass("Middle East Standard Time", "Middle East Daylight Time", "(UTC+02:00) Beirut", "Middle East Standard Time", "02:00:00"),
                new TimeZonesClass("Egypt Standard Time", "Egypt Daylight Time", "(UTC+02:00) Cairo", "Egypt Standard Time", "02:00:00"),
                new TimeZonesClass("Syria Standard Time", "Syria Daylight Time", "(UTC+02:00) Damascus", "Syria Standard Time", "02:00:00"),
                new TimeZonesClass("E. Europe Standard Time", "E. Europe Daylight Time", "(UTC+02:00) E. Europe", "E. Europe Standard Time", "02:00:00"),
                new TimeZonesClass("South Africa Standard Time", "South Africa Daylight Time", "(UTC+02:00) Harare, Pretoria", "South Africa Standard Time", "02:00:00"),
                new TimeZonesClass("FLE Standard Time", "FLE Daylight Time", "(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius", "FLE Standard Time", "02:00:00"),
                new TimeZonesClass("Turkey Standard Time", "Turkey Daylight Time", "(UTC+02:00) Istanbul", "Turkey Standard Time", "02:00:00"),
                new TimeZonesClass("Israel Standard Time", "Jerusalem Daylight Time", "(UTC+02:00) Jerusalem", "Jerusalem Standard Time", "02:00:00"),
                new TimeZonesClass("Kaliningrad Standard Time", "Russia TZ 1 Daylight Time", "(UTC+02:00) Kaliningrad (RTZ 1)", "Russia TZ 1 Standard Time", "02:00:00"),
                new TimeZonesClass("Libya Standard Time", "Libya Daylight Time", "(UTC+02:00) Tripoli", "Libya Standard Time", "02:00:00"),
                new TimeZonesClass("Arabic Standard Time", "Arabic Daylight Time", "(UTC+03:00) Baghdad", "Arabic Standard Time", "03:00:00"),
                new TimeZonesClass("Arab Standard Time", "Arab Daylight Time", "(UTC+03:00) Kuwait, Riyadh", "Arab Standard Time", "03:00:00"),
                new TimeZonesClass("Belarus Standard Time", "Belarus Daylight Time", "(UTC+03:00) Minsk", "Belarus Standard Time", "03:00:00"),
                new TimeZonesClass("Russian Standard Time", "Russia TZ 2 Daylight Time", "(UTC+03:00) Moscow, St. Petersburg, Volgograd (RTZ 2)", "Russia TZ 2 Standard Time", "03:00:00"),
                new TimeZonesClass("E. Africa Standard Time", "E. Africa Daylight Time", "(UTC+03:00) Nairobi", "E. Africa Standard Time", "03:00:00"),
                new TimeZonesClass("Iran Standard Time", "Iran Daylight Time", "(UTC+03:30) Tehran", "Iran Standard Time", "03:30:00"),
                new TimeZonesClass("Arabian Standard Time", "Arabian Daylight Time", "(UTC+04:00) Abu Dhabi, Muscat", "Arabian Standard Time", "04:00:00"),
                new TimeZonesClass("Azerbaijan Standard Time", "Azerbaijan Daylight Time", "(UTC+04:00) Baku", "Azerbaijan Standard Time", "04:00:00"),
                new TimeZonesClass("Russia Time Zone 3", "Russia TZ 3 Daylight Time", "(UTC+04:00) Izhevsk, Samara (RTZ 3)", "Russia TZ 3 Standard Time", "04:00:00"),
                new TimeZonesClass("Mauritius Standard Time", "Mauritius Daylight Time", "(UTC+04:00) Port Louis", "Mauritius Standard Time", "04:00:00"),
                new TimeZonesClass("Georgian Standard Time", "Georgian Daylight Time", "(UTC+04:00) Tbilisi", "Georgian Standard Time", "04:00:00"),
                new TimeZonesClass("Caucasus Standard Time", "Caucasus Daylight Time", "(UTC+04:00) Yerevan", "Caucasus Standard Time", "04:00:00"),
                new TimeZonesClass("Afghanistan Standard Time", "Afghanistan Daylight Time", "(UTC+04:30) Kabul", "Afghanistan Standard Time", "04:30:00"),
                new TimeZonesClass("West Asia Standard Time", "West Asia Daylight Time", "(UTC+05:00) Ashgabat, Tashkent", "West Asia Standard Time", "05:00:00"),
                new TimeZonesClass("Ekaterinburg Standard Time", "Russia TZ 4 Daylight Time", "(UTC+05:00) Ekaterinburg (RTZ 4)", "Russia TZ 4 Standard Time", "05:00:00"),
                new TimeZonesClass("Pakistan Standard Time", "Pakistan Daylight Time", "(UTC+05:00) Islamabad, Karachi", "Pakistan Standard Time", "05:00:00"),
                new TimeZonesClass("India Standard Time", "India Daylight Time", "(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi", "India Standard Time", "05:30:00"),
                new TimeZonesClass("Sri Lanka Standard Time", "Sri Lanka Daylight Time", "(UTC+05:30) Sri Jayawardenepura", "Sri Lanka Standard Time", "05:30:00"),
                new TimeZonesClass("Nepal Standard Time", "Nepal Daylight Time", "(UTC+05:45) Kathmandu", "Nepal Standard Time", "05:45:00"),
                new TimeZonesClass("Central Asia Standard Time", "Central Asia Daylight Time", "(UTC+06:00) Astana", "Central Asia Standard Time", "06:00:00"),
                new TimeZonesClass("Bangladesh Standard Time", "Bangladesh Daylight Time", "(UTC+06:00) Dhaka", "Bangladesh Standard Time", "06:00:00"),
                new TimeZonesClass("N. Central Asia Standard Time", "Russia TZ 5 Daylight Time", "(UTC+06:00) Novosibirsk (RTZ 5)", "Russia TZ 5 Standard Time", "06:00:00"),
                new TimeZonesClass("Myanmar Standard Time", "Myanmar Daylight Time", "(UTC+06:30) Yangon (Rangoon)", "Myanmar Standard Time", "06:30:00"),
                new TimeZonesClass("SE Asia Standard Time", "SE Asia Daylight Time", "(UTC+07:00) Bangkok, Hanoi, Jakarta", "SE Asia Standard Time", "07:00:00"),
                new TimeZonesClass("North Asia Standard Time", "Russia TZ 6 Daylight Time", "(UTC+07:00) Krasnoyarsk (RTZ 6)", "Russia TZ 6 Standard Time", "07:00:00"),
                new TimeZonesClass("China Standard Time", "China Daylight Time", "(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi", "China Standard Time", "08:00:00"),
                new TimeZonesClass("North Asia East Standard Time", "Russia TZ 7 Daylight Time", "(UTC+08:00) Irkutsk (RTZ 7)", "Russia TZ 7 Standard Time", "08:00:00"),
                new TimeZonesClass("Singapore Standard Time", "Malay Peninsula Daylight Time", "(UTC+08:00) Kuala Lumpur, Singapore", "Malay Peninsula Standard Time", "08:00:00"),
                new TimeZonesClass("W. Australia Standard Time", "W. Australia Daylight Time", "(UTC+08:00) Perth", "W. Australia Standard Time", "08:00:00"),
                new TimeZonesClass("Taipei Standard Time", "Taipei Daylight Time", "(UTC+08:00) Taipei", "Taipei Standard Time", "08:00:00"),
                new TimeZonesClass("Ulaanbaatar Standard Time", "Ulaanbaatar Daylight Time", "(UTC+08:00) Ulaanbaatar", "Ulaanbaatar Standard Time", "08:00:00"),
                new TimeZonesClass("North Korea Standard Time", "North Korea Daylight Time", "(UTC+08:30) Pyongyang", "North Korea Standard Time", "08:30:00"),
                new TimeZonesClass("Tokyo Standard Time", "Tokyo Daylight Time", "(UTC+09:00) Osaka, Sapporo, Tokyo", "Tokyo Standard Time", "09:00:00"),
                new TimeZonesClass("Korea Standard Time", "Korea Daylight Time", "(UTC+09:00) Seoul", "Korea Standard Time", "09:00:00"),
                new TimeZonesClass("Yakutsk Standard Time", "Russia TZ 8 Daylight Time", "(UTC+09:00) Yakutsk (RTZ 8)", "Russia TZ 8 Standard Time", "09:00:00"),
                new TimeZonesClass("Cen. Australia Standard Time", "Cen. Australia Daylight Time", "(UTC+09:30) Adelaide", "Cen. Australia Standard Time", "09:30:00"),
                new TimeZonesClass("AUS Central Standard Time", "AUS Central Daylight Time", "(UTC+09:30) Darwin", "AUS Central Standard Time", "09:30:00"),
                new TimeZonesClass("E. Australia Standard Time", "E. Australia Daylight Time", "(UTC+10:00) Brisbane", "E. Australia Standard Time", "10:00:00"),
                new TimeZonesClass("AUS Eastern Standard Time", "AUS Eastern Daylight Time", "(UTC+10:00) Canberra, Melbourne, Sydney", "AUS Eastern Standard Time", "10:00:00"),
                new TimeZonesClass("West Pacific Standard Time", "West Pacific Daylight Time", "(UTC+10:00) Guam, Port Moresby", "West Pacific Standard Time", "10:00:00"),
                new TimeZonesClass("Tasmania Standard Time", "Tasmania Daylight Time", "(UTC+10:00) Hobart", "Tasmania Standard Time", "10:00:00"),
                new TimeZonesClass("Magadan Standard Time", "Magadan Daylight Time", "(UTC+10:00) Magadan", "Magadan Standard Time", "10:00:00"),
                new TimeZonesClass("Vladivostok Standard Time", "Russia TZ 9 Daylight Time", "(UTC+10:00) Vladivostok, Magadan (RTZ 9)", "Russia TZ 9 Standard Time", "10:00:00"),
                new TimeZonesClass("Russia Time Zone 10", "Russia TZ 10 Daylight Time", "(UTC+11:00) Chokurdakh (RTZ 10)", "Russia TZ 10 Standard Time", "11:00:00"),
                new TimeZonesClass("Central Pacific Standard Time", "Central Pacific Daylight Time", "(UTC+11:00) Solomon Is., New Caledonia", "Central Pacific Standard Time", "11:00:00"),
                new TimeZonesClass("Russia Time Zone 11", "Russia TZ 11 Daylight Time", "(UTC+12:00) Anadyr, Petropavlovsk-Kamchatsky (RTZ 11)", "Russia TZ 11 Standard Time", "12:00:00"),
                new TimeZonesClass("New Zealand Standard Time", "New Zealand Daylight Time", "(UTC+12:00) Auckland, Wellington", "New Zealand Standard Time", "12:00:00"),
                new TimeZonesClass("UTC+12", "UTC+12", "(UTC+12:00) Coordinated Universal Time+12", "UTC+12", "12:00:00"),
                new TimeZonesClass("Fiji Standard Time", "Fiji Daylight Time", "(UTC+12:00) Fiji", "Fiji Standard Time", "12:00:00"),
                new TimeZonesClass("Kamchatka Standard Time", "Kamchatka Daylight Time", "(UTC+12:00) Petropavlovsk-Kamchatsky - Old", "Kamchatka Standard Time", "12:00:00"),
                new TimeZonesClass("Tonga Standard Time", "Tonga Daylight Time", "(UTC+13:00) Nuku'alofa", "Tonga Standard Time", "13:00:00"),
                new TimeZonesClass("Samoa Standard Time", "Samoa Daylight Time", "(UTC+13:00) Samoa", "Samoa Standard Time", "13:00:00"),
                new TimeZonesClass("Line Islands Standard Time", "Line Islands Daylight Time", "(UTC+14:00) Kiritimati Island", "Line Islands Standard Time", "14:00:00")
            };
            return Zones;
        }
    }
}
