
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLCitas.DataLayer.DoctorsRepository
{
    public class CitasByDoctor
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public Nullable<DateTime> start_date { get; set; }
        public Nullable<DateTime> end_date { get; set; }
        public string end { get; set; }
        public string start { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<int> status_id { get; set; }
        public string color { get; set; }
        public System.Guid office_uId { get; set; }
        public System.Guid consultory_uid { get; set; }
        public bool durationEditable { get; internal set; }
        public string url { get; set; }
        public Nullable<Guid> doctor_uid { get; set; }
        public string doctor_name { get; set; }
    }

    public class DoctorConfigurationModel
    {
        public DoctorConfigurationModel() { }
        public DoctorConfigurationModel(Doctor_Configs Model) { }
    }
}