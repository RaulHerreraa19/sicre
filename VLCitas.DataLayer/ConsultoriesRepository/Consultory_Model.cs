using System;

namespace VLCitas.DataLayer.ConsultoriesRepository
{
    public class Consultory_Model
    {
        public Consultory_Model(){ }
        public Consultory_Model(Consultory Model){ ParseModel(Model); }
        private void ParseModel(Consultory Model){
            uId = Model.uId;
            name = Model.name;
            description = Model.description;
            office_uId = Model.office_uId;
            alias = Model.alias;
            status_id = Model.status_id;
            image_url = Model.image_url;
            create_date = Model.create_date;
            update_date = Model.update_date;
        }

        public System.Guid uId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<System.Guid> office_uId { get; set; }
        public string alias { get; set; }
        public Nullable<int> status_id { get; set; }
        public string image_url { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public string serie { get; set; }
    }
}