using System;
using System.Linq;
using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.Models;
using VLCitas.DataLayer.Tools;

namespace VLCitas.DataLayer.UsersRepository
{
    public class UserRepository
    {
        private Cryptography cp;

        public UserRepository()
        {
            cp = new Cryptography();
        }

        public Users ValidateUser(string email, string pwd)
        {
            try
            {
                VL_CitasEntities db = new VL_CitasEntities();
                pwd = cp.Encrypt(pwd, true);
                Users user = db.Users.Where(x => x.email == email && x.password == pwd).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("ValidateUser Error:" + ex.ToString());
                return null;
            }

        }

        public Response UpdateAboutYou(AboutYouModel model)
        {
            Response res = new Response() { Data = null, Message = "Success", TypeOfResponse = TypeOfResponse.OK };
            try
            {
                VL_CitasEntities db = new VL_CitasEntities();
                Users user = db.Users.Where(x => x.uId == model.uId).FirstOrDefault();
                user.first_name = model.first_name;
                user.last_name = model.last_name;
                user.address = model.address;
                user.phone = model.phone;
                user.email = model.email;
                user.Doctor_Configs.professional_license = model.professional_license;
                user.Doctor_Configs.webpage = model.webpage;
                user.Doctor_Configs.facebook = model.facebook;
                user.Doctor_Configs.twitter = model.twitter;
                user.Doctor_Configs.instagram = model.instagram;
                user.Doctor_Configs.resume = model.resume;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("UserRepository - UpdateAboutYou - Error: " + ex.ToString());
                res.Message = ex.Message;
                res.TypeOfResponse = TypeOfResponse.ErrorResponse;
            }
            return res;
        }

        public Response UpdateSpecialities(User_specialitiesModel model)
        {
            Response res = new Response() { Data = null, Message = "Success", TypeOfResponse = TypeOfResponse.OK };
            try
            {
                VL_CitasEntities db = new VL_CitasEntities();
                Users user = db.Users.Where(x => x.uId == model.uId).FirstOrDefault();
                user.Doctor_Configs.Specialties.Clear();
                db.SaveChanges();
                if (model.specialies != null)
                {
                    var specialities = db.Specialties.Where(x => model.specialies.Contains(x.id)).ToList();
                    foreach (var item in specialities)
                    {
                        user.Doctor_Configs.Specialties.Add(item);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("UserRepository - UpdateSpecialities - Error: " + ex.ToString());
                res.Message = ex.Message;
                res.TypeOfResponse = TypeOfResponse.ErrorResponse;
            }
            return res;
        }

        public Response UpdateClinicInfo(ClinicInfoModel model)
        {
            Response res = new Response() { Data = null, Message = "Success", TypeOfResponse = TypeOfResponse.OK };
            try
            {
                VL_CitasEntities db = new VL_CitasEntities();
                Offices_Users user_office = db.Offices_Users.Where(x => x.user_uid == model.uId && x.office_uid == model.office_uid).FirstOrDefault();
                user_office.price_per_appoinment = model.price_per_appoinment;
                user_office.medical_appointment_duration = model.medical_appointment_duration;
                db.SaveChanges();
                var user = db.Users.Where(x => x.uId == model.uId).FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("UserRepository - UpdateSpecialities - Error: " + ex.ToString());
                res.Message = ex.Message;
                res.TypeOfResponse = TypeOfResponse.ErrorResponse;
            }
            return res;
        }

        public bool IsSelfDoctor(Users User)
        {
            bool Is = false;
            try
            {
                //VL_CitasEntities db = new VL_CitasEntities();
                //User.Consultory
                
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("IsSelfDoctor Error: " + ex.ToString());
            }
            return Is;
        }
    }
}