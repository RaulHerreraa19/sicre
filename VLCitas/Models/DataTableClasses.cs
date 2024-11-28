using DataTables;
using System;

namespace VLCitas.Models
{
    public class DoctorConfigModel : EditorModel
    {
        public int id { get; set; }
        public string professional_license { get; set; }
        public string resume { get; set; }
        public string job_description { get; set; }
        public Nullable<int> logo1 { get; set; }
        public Nullable<int> logo2 { get; set; }
        public Nullable<int> logo3 { get; set; }
        public Nullable<int> logo4 { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string instagram { get; set; }
        public string webpage { get; set; }
    }

    public class ImageModel : EditorModel
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string webPath { get; set; }
        public string systemPath { get; set; }
    }
}