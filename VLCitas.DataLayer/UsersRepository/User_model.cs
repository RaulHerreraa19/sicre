using System;

namespace VLCitas.DataLayer.UsersRepository
{
    public class User_Model
    {
        public User_Model() { }
        public User_Model(Users Model) { ParseModel(Model); }
        public void ParseModel(Users Model)
        {
            uId = Model.uId;
            first_name = Model.first_name;
            last_name = Model.last_name;
            email = Model.email;
            address = Model.address;
            phone = Model.phone;
            profile_picture = Model.profile_picture;
            doctor_id = Model.doctor_id;
        }
        public System.Guid uId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<int> status_id { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public Nullable<int> profile_picture { get; set; }
        public Nullable<int> doctor_id { get; set; }
    }
    
 
}