using VLCitas.DataLayer.CommonRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace VLCitas.DataLayer.PatientsRepository
{
    public class PatientRepositoy
    {
        public PatientRepositoy(){}

        public Paciente FindPaciente(int id)
        {
            Paciente res = new Paciente();
            try
            {
                VL_CitasEntities db = new VL_CitasEntities();
                res = db.Paciente.Find(id);
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("PatientModel - FindPaciente - Error: " + ex.ToString());
            }
            return res;
        }

        public List<Paciente> FindPaciente(string keyword, Guid uId)
        {
            List<Paciente> res = new List<Paciente>();
            try
            {
                VL_CitasEntities db = new VL_CitasEntities();
                List<int?> pacientesIds = db.Citas.Where(x => x.status_id == 3 && x.doctor_uid == uId).Select(c => c.id_paciente).Distinct().ToList();
                res = db.Paciente.Where(x => pacientesIds.Contains(x.id)).ToList();
                res = res.Where(x => x.telefono.Contains(keyword) || x.nombre.ToUpper().Contains(keyword)).ToList();
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("PatientModel - FindPaciente - Error: " + ex.ToString());
            }
            return res;
        }

        public List<Paciente> RecentPaciente(Guid uId)
        {
            List<Paciente> res = new List<Paciente>();
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
                var pacientesIds = db.Citas.Where(x => x.status_id == 3 && x.doctor_uid == uId).OrderByDescending(x=> x.cita_date).Take(10).Select(c => c.id_paciente).Distinct().ToList();
                res = db.Paciente.Where(x => pacientesIds.Contains(x.id)).ToList();
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("PatientModel - FindPaciente - Error: " + ex.ToString());
            }
            return res;
        }

        public Patient_Model AddNewPatient(Patient_Model model)
        {
            try
            {
                VL_CitasEntities db = new VL_CitasEntities();
                Paciente patient = new Paciente(model);
                db.Paciente.Add(patient);
                db.SaveChanges();
                model.id = patient.id;
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("PatientModel - AddNewPatient - Error:" + ex.ToString());
            }
            return model;
        }

        public bool isAvailableDoctor(int id, Guid uId)
        {
            bool res = false;
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
                var citas = db.Citas.Where(x => x.doctor_uid == uId && x.id_paciente == id && (x.status_id == 1 || x.status_id ==3)).Count();
                if (citas > 0)
                    res = true;
            }
            catch (Exception ex)
            {
                res = false;
                Common.Set_Log_Errors("PatientModel - isAvailableDoctor - Error: " + ex.ToString());
            }
            return res;
        }
    }

}