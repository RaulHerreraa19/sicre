using VLCitas.DataLayer.CommonRepository;
using System;
using System.Linq;

namespace VLCitas.DataLayer.OfficesRepository
{
    public class Configuration
    {
        public Configuration() { }
        public Configuration(Offices_Configuration Model)
        {
            ParseModel(Model);
        }

        private void ParseModel(Offices_Configuration Model)
        {
            id = Model.id;
            time_zone_id = Model.time_zone_id;
            if (Model.TimeZones != null)
            {
                TimeZoneId = Model.TimeZones.TimeZoneId;
            }
        }

        public int id { get; set; }
        public int time_zone_id { get; set; }
        private string TimeZoneId;

        public DateTime Now
        {
            get
            {
                DateTime time = DateTime.UtcNow;
                try
                {
                    if (TimeZoneId == null)
                    {
                        VL_CitasEntities db = new VL_CitasEntities();
                        TimeZones TimeZone = db.TimeZones.Where(x => x.id == time_zone_id).FirstOrDefault();
                        if (TimeZone != null)
                            TimeZoneId = TimeZone.TimeZoneId;
                    }
                    time = TimeZonesClass.GetDateByTimeZone(time, TimeZoneId);
                }
                catch (Exception ex)
                {
                    Common.Set_Log_Errors("GetTimeByZone time_zone_id:" + time_zone_id.ToString() + " Error: " + ex.ToString());
                    time = DateTime.Now;
                }
                return time;
            }
        }
    }


}