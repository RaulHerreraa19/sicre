using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLCitas.DataLayer.OfficesRepository
{
    public class OfficeModel
    {
        public System.Guid uId { get; set; }
        public string name { get; set; }
    }

    public class Offices_model
    {
        public Offices_model() { }
        public Offices_model(Offices Model)
        {
            ParseModel(Model);
        }

        private void ParseModel(Offices Model)
        {
            uId = Model.uId;
            name = Model.name;
            address = Model.address;
            status_id = Model.status_id;
            customer_uId = Model.customer_uId;
            image_url = Model.image_url;
            alias = Model.alias;
            create_date = Model.create_date;
            update_date = Model.update_date;
            latitud = Model.latitud;
            longitud = Model.longitud;
            configuration_id = Model.configuration_id;
            phone = Model.phone;
            phone_2 = Model.phone_2;
            if (configuration_id != null)
                configuration = new Configuration(Model.Offices_Configuration);
            else  //Default Configuration
                configuration = new Configuration() {time_zone_id = 12 };
        }

        public System.Guid uId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Nullable<int> status_id { get; set; }
        public Nullable<System.Guid> customer_uId { get; set; }
        public string image_url { get; set; }
        public string alias { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string phone { get; set; }
        public string phone_2 { get; set; }
        public Nullable<int> configuration_id { get; set; }
        public Configuration configuration { get; set; }
    }
}