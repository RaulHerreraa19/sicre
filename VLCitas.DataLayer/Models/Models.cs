using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.DoctorsRepository;
using VLCitas.DataLayer.SchedulesRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace VLCitas.DataLayer.Models
{
 
    public class DataView
    {
        public string view_name;
        public string controller_name;
    }

    public class RolesModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

   

    public class ConfigImagesModel
    {
        public int config_id { get; set; }
        public int image_id { get; set; }
    }

    public class SpecialityModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class VacationsModel
    {
        public int id { get; set; }
        public Guid? user_uid { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public string reason { get; set; }
    }

    public class AboutYouModel
    {
        public Guid uId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string professional_license { get; set; }
        public string webpage { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string instagram { get; set; }
        public string resume { get; set; }
    }

    public class ClinicInfoModel
    {
        public Guid uId { get; set; }
        public decimal price_per_appoinment { get; set; }
        public int medical_appointment_duration { get; set; }
        public Guid office_uid { get; set; }
    }

    public class User_specialitiesModel
    {
        public Guid uId { get; set; }
        public List<int> specialies { get; set; }
    }

    public class ServicesModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public Nullable<double> precio { get; set; }
        public string descripcion { get; set; }
        public Nullable<bool> activo { get; set; }
        public Nullable<System.Guid> user_uId { get; set; }
    }

    public class AdditinalInfo
    {
        public AdditinalInfo() { }
        public AdditinalInfo(List<CitasByDoctor> citas, Offices_Users userOfficeConfig, DateTime now)
        {
            try
            {
                duration = 30;
                price = 30;
                if (userOfficeConfig != null)
                {
                    duration = userOfficeConfig.medical_appointment_duration == null ? 30 : (int)userOfficeConfig.medical_appointment_duration;
                    price = userOfficeConfig.price_per_appoinment;
                }
                DateTime start = now.Date;
                DateTime end = now.Date.AddDays(1);
                today = citas.Count(x => x.start_date >= start && x.start_date <= end);
                int todayCompleted = citas.Count(x => x.start_date >= start && x.start_date <= end && x.status_id == 3);
                progressToday = todayCompleted == 0 ? 0:(todayCompleted * 100) / today;
                month = citas.Count();
                int monthCompleted = citas.Count(x => x.status_id == 3);
                progressMonth = monthCompleted ==  0?0:(monthCompleted * 100) / month;
                revenueToday = (price * today).ToString("C", CultureInfo.CurrentCulture);
                revenueMonth = (price * month).ToString("C", CultureInfo.CurrentCulture);
                slotDuration = duration < 10 ? "0" + duration.ToString() : duration.ToString();
                slotDuration = "00:" + slotDuration + ":00";
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("AdditinalInfo Error:" + ex.ToString());
            }
        }
        public int today;
        public int progressToday;
        public int month;
        public int progressMonth;
        public decimal price;
        public int duration;
        public string slotDuration;
        public string revenueMonth;
        public string revenueToday;
    }

    public class ListOfItems
    {
        public ListOfItems() { }
        public ListOfItems(string _viewName, string _controllerName, IEnumerable<Consultory> _items, List<CitasByDoctor> citas)
        {
            SetGeneralData(_viewName, _controllerName);
            items = new List<Item>();
            foreach (Consultory item in _items)
            {
                items.Add(new Item()
                {
                    id = item.uId.ToString(),
                    value1 = item.name,
                    value2 = citas.Count(x=>x.consultory_uid == item.uId).ToString(),
                    value3 = item.description
                });
            }
        }

        public ListOfItems(string _viewName, string _controllerName, IEnumerable<Users> _items, List<CitasByDoctor> citas)
        {
            SetGeneralData(_viewName, _controllerName);
            items = new List<Item>();
            Guid office_uId = citas.Select(x => x.office_uId).FirstOrDefault();
            foreach (Users item in _items)
            {
                string value3 = item.Doctor_Configs.Specialties.Count>0 ? item.Doctor_Configs.Specialties.Select(x => x.name).FirstOrDefault() : Resource.Doctor;
                var schedules = item.Offices_Users.Where(x => x.office_uid == office_uId).Select(x => x.Schedule).FirstOrDefault();
                items.Add(new Item()
                {
                    id = item.uId.ToString(),
                    value1 = item.first_name + " " + item.last_name,
                    value2 = citas.Count(x => x.doctor_uid == item.uId).ToString(),
                    value3 = value3,
                    data = schedules.Select(x=> new Schedule_Model(x))
                });
            }
        }

        private void SetGeneralData(string _viewName, string _controllerName)
        {
            viewName = _viewName;
            controllerName = _controllerName;
        }

        public string viewName;
        public string controllerName;
        public List<Item> items;
    }
    public class Item
    {
        public string id;
        public string value1;
        public string value2;
        public string value3;
        public string value4;
        public object data;
    }
}