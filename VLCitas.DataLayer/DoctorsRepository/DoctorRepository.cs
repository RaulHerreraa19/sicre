using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.PatientsRepository;
using VLCitas.DataLayer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLCitas.DataLayer.DoctorsRepository
{
    public class DoctorRepository
    {
        private VL_CitasEntities db;
        private Repository<Paciente> repoPatient;
        //Repository<GetCitasByDoctor_Result> repo = new Repository<GetCitasByDoctor_Result>();
        public DoctorRepository()
        {
             db = new VL_CitasEntities();
            repoPatient = new Repository<Paciente>();
        }

        public Doctor_Configs GetConfiguration(Guid user_uId)
        {
            Doctor_Configs configuration = null;
            try
            {
                configuration = db.Users.Where(x => x.uId == user_uId && x.doctor_id != null).Select(x=>x.Doctor_Configs).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("GetConfiguration -> Error:" + ex.ToString());
            }
            return configuration;
        }

        public IEnumerable<Patient_Model> GetPatients(Guid doctor_uId)
        {
            IEnumerable<Patient_Model> result = null;
            try
            {
                result = repoPatient.Get(x => x.Citas.Where(y => y.doctor_uid == doctor_uId).Count() > 0).OrderByDescending(x=>x.created_date).Take(15).Select(x => new Patient_Model(x));
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("GetConfiguration -> Error:" + ex.ToString());
            }
            return result;
        }

        public IEnumerable<Patient_Model> SearchPatients(string data)
        {
            IEnumerable<Patient_Model> result = null;
            try
            {
                result = repoPatient.Get(x => x.nombre.Contains(data) || x.telefono.Contains(data) || x.email.Contains(data) || x.curp.Contains(data)).OrderByDescending(x => x.created_date).Take(30).Select(x => new Patient_Model(x));
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("GetConfiguration -> Error:" + ex.ToString());
            }
            return result;
        }
    }
}