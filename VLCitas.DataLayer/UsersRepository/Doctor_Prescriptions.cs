using System;
using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.OfficesRepository;

namespace VLCitas.DataLayer.UsersRepository
{
    public class DoctorPrescription
    {
        public DoctorPrescription() { }
        public DoctorPrescription(Users Model, Offices_model office)
        {
            ParseModel(Model, office);
        }
        private void ParseModel(Users Model, Offices_model office)
        {
            try
            {
                name = Model.first_name + " " + Model.last_name;
                phone = Model.phone;
                email = Model.email;
                foreach (Specialties specialty in Model.Doctor_Configs.Specialties)
                    specialties += specialty.name + "";
                specialties = string.IsNullOrEmpty(specialties)?"":specialties.Trim();
                job_description = Model.Doctor_Configs.job_description;
                logo1 = Model.Doctor_Configs.logo1;
                logo2 = Model.Doctor_Configs.logo2;
                logo3 = Model.Doctor_Configs.logo3;
                logo4 = Model.Doctor_Configs.logo4;
                consultory_name = office.name;
                consultory_address = office.address;
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("DoctorPrescription Error:" + ex.ToString());
            }
        }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string consultory_address { get; set; }
        public string consultory_name { get; set; }
        public string consultory_phones { get; set; }
        public string specialties { get; set; }
        public string professional_license { get; set; }
        public string job_description { get; set; }
        public Nullable<int> logo1 { get; set; }
        public Nullable<int> logo2 { get; set; }
        public Nullable<int> logo3 { get; set; }
        public Nullable<int> logo4 { get; set; }
    }
}