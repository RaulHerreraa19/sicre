using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.DoctorsRepository;
using VLCitas.DataLayer.Models;
using VLCitas.DataLayer.OfficesRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace VLCitas.DataLayer.CitasRepository
{
    public class CitaRepository
    {
        private VL_CitasEntities db;
        //private Repository<Citas> citaRepo;
        public CitaRepository()
        {
            this.db = new VL_CitasEntities();
            //citaRepo = new Repository<Citas>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        /// 
        public Response ReSchedule(Cita_Model appointment, Offices_model office)
        {
            Response response = new Response();
            try
            {
                Citas cita = db.Citas.Find(appointment.uId);
                cita.cita_date = appointment.cita_date;
                cita.status_id = 1;
                db.SaveChanges();
                response.Message = "La cita se ha re agendado correctamente";
                response.TypeOfResponse = TypeOfResponse.OK;
                var userInOffice = db.Offices_Users.Where(x => x.office_uid == office.uId && x.user_uid == cita.doctor_uid).FirstOrDefault();
                userInOffice.medical_appointment_duration = userInOffice.medical_appointment_duration == null ? 30 : userInOffice.medical_appointment_duration;
                double time_prom = Convert.ToDouble(userInOffice.medical_appointment_duration);
                response.Data = new CitasByDoctor
                {
                    id = cita.uId,
                    title = cita.title,
                    start_date = cita.cita_date,
                    start = cita.cita_date.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end_date = cita.cita_date.Value.AddMinutes(time_prom),
                    end = cita.cita_date.Value.AddMinutes(time_prom).ToString("yyyy-MM-ddTHH:mm:ss"),
                    durationEditable = false,
                    status_id = cita.status_id,
                    color = cita.Status_Citas.color,
                    consultory_uid = (Guid)cita.consultory_uId,
                    precio = cita.precio,
                    office_uId = office.uId,
                    doctor_uid = cita.doctor_uid,
                    doctor_name = cita.Users.first_name+" "+cita.Users.last_name
                };
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("ReSchedule Error: " + ex.ToString());
                response.TypeOfResponse = TypeOfResponse.ErrorResponse;
            }
            return response;
        }

        public Citas getLastCita(int? id_paciente, Guid? doctor_uid)
        {
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
                Citas cita = db.Citas.Where(x => x.id_paciente == id_paciente && x.doctor_uid == doctor_uid && x.status_id == 3).OrderByDescending(y => y.cita_date).FirstOrDefault();
                if (cita == null)
                {
                    cita = new Citas();
                }
                return cita;
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("GetCita Error: " + ex.ToString());
                return new Citas();
            }
        }

        public Response SetExtraDataPaciente(int id, int action, string data)
        {
            Response response = new Response();
            try
            {
                Paciente pa = db.Paciente.Where(x => x.id == id).FirstOrDefault();
                if (pa!= null)
                {
                    switch (action)
                    {
                        case 1:
                            pa.fecha_nacimiento = Convert.ToDateTime(data);
                            db.SaveChanges();
                            response.TypeOfResponse = TypeOfResponse.OK;
                            response.Message = "La fecha de nacimiento se ha guardado correctamente";
                            response.Data = pa.fecha_nacimiento;
                            break;
                        case 2:
                            pa.alergias = data;
                            db.SaveChanges();
                            response.TypeOfResponse = TypeOfResponse.OK;
                            response.Message = "Las alergias se ha guardado correctamente";
                            response.Data = pa.alergias;
                            break;
                        case 3:
                            pa.cirugias = data;
                            db.SaveChanges();
                            response.TypeOfResponse = TypeOfResponse.OK;
                            response.Message = "Las cirugias se ha guardado correctamente";
                            response.Data = pa.alergias;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("SetExtraDataPaciente Error: " + ex.ToString());
                response.TypeOfResponse = TypeOfResponse.ErrorResponse;
            }
            return response;
        }

        public Citas GetCita(Guid cita_uid)
        {
            try
            {
                Citas cita = db.Citas.Find(cita_uid);
                return cita;
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("GetCita Error: " + ex.ToString());
                return new Citas();
            }
        }

        public Response Add(Cita_Model appointment, Offices_model office)
        {
            Response response = new Response();
            try
            {
                if (appointment.id_patient!= null)
                {
                    VL_CitasEntities db = new VL_CitasEntities();
                    var userInOffice = db.Offices_Users.Where(x => x.office_uid == office.uId && x.user_uid == appointment.doctor_uid).FirstOrDefault();
                    appointment.uId = Guid.NewGuid();
                    appointment.precio = userInOffice.price_per_appoinment;
                    appointment.create_date = office.configuration.Now;
                    Citas cita = new Citas(appointment);
                    db.Citas.Add(cita);
                    db.SaveChanges();
                    cita = db.Citas.AsNoTracking().Where(x => x.uId == cita.uId).FirstOrDefault();
                    response.TypeOfResponse = TypeOfResponse.OK;
                    response.Message = Resource.AppointmentHasBeenScheduled;

                    userInOffice.medical_appointment_duration = userInOffice.medical_appointment_duration == null ? 30 : userInOffice.medical_appointment_duration;
                    double time_prom = Convert.ToDouble(userInOffice.medical_appointment_duration);
                    response.Data = new CitasByDoctor
                    {
                        id = cita.uId,
                        title = cita.title,
                        start_date = cita.cita_date,
                        start = cita.cita_date.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                        end_date = cita.cita_date.Value.AddMinutes(time_prom),
                        end = cita.cita_date.Value.AddMinutes(time_prom).ToString("yyyy-MM-ddTHH:mm:ss"),
                        durationEditable = false,
                        status_id = cita.status_id,
                        color = cita.Status_Citas.color,
                        consultory_uid = (Guid)appointment.consultory_uId,
                        precio = appointment.precio,
                        office_uId = office.uId,
                        doctor_uid = cita.doctor_uid,
                        doctor_name = cita.Users.first_name + " " + cita.Users.last_name
                    };
                    TimeSpan tspan = cita.cita_date.Value - cita.create_date.Value;
                    Consultory Consultory = db.Consultory.Where(x => x.uId == appointment.consultory_uId).FirstOrDefault();
                    Paciente patient = db.Paciente.Where(x => x.id == appointment.id_patient).FirstOrDefault();
                    CultureInfo cultureInfo = new CultureInfo(Settings.Language.LanguageCulture);
                    string body = "";
                    if (tspan.Days >= 1)
                        body = string.Format(Resource.Message_AgendarCita, patient.nombre, string.Format(cultureInfo, "{0:f}", cita.cita_date), Consultory.name);
                    else
                        body = string.Format(Resource.Message_AgendarYConfirmarCita, patient.nombre, string.Format(cultureInfo, "{0:f}", cita.cita_date), Consultory.name, Settings.Url + "/citas/confirmar?uid=" + cita.uId.ToString());

                    ThreadPool.QueueUserWorkItem(sender => Common.notifyCitaCreated(body, cita.Consultory.name, patient.email));
                }
                else
                {
                    response.Message = "Invalid User";
                }
                
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("Addcita Error: " + ex.ToString());
                response.TypeOfResponse = TypeOfResponse.ErrorResponse;
                response.Message = "Excepction:" + ex.Message;
            }
            return response;
        }

        public Response RemoveMedicine(int id)
        {
            Response response = new Response() { TypeOfResponse = TypeOfResponse.OK, Message = "Success" };
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
                var medicine = db.Medicine_citas.Where(x => x.id == id).FirstOrDefault();
               
                db.Medicine_citas.Remove(medicine);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("DeleteCita Error: " + ex.ToString());
                response.TypeOfResponse = TypeOfResponse.ErrorResponse;
                response.Message = "Excepction " + ex.Message;
            }
            return response;
        }

        public Response AddMedicine(Medicine_citas medicine)
        {
            Response response = new Response() { TypeOfResponse = TypeOfResponse.OK, Message = "Success" };
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
                medicine.sentence = medicine.medicine != null ? medicine.medicine.Trim() : "";
                if (!String.IsNullOrEmpty(medicine.dose))
                {
                    medicine.sentence = medicine.sentence + ", " + Resource.Dose + ": " + medicine.dose;
                }
                if (!String.IsNullOrEmpty(medicine.frequency))
                {
                    medicine.sentence = medicine.sentence + " " + Resource.Each + " " + medicine.frequency;
                }
                if (!String.IsNullOrEmpty(medicine.duration))
                {
                    medicine.sentence = medicine.sentence + " " + Resource.During + " " + medicine.duration +". ";
                }
                if (!String.IsNullOrEmpty(medicine.extra))
                {
                    medicine.sentence = medicine.sentence + medicine.extra;
                }
                db.Medicine_citas.Add(medicine);
                db.SaveChanges();
                response.Data = new
                {
                    medicine.id,
                    medicine.sentence
                }; 
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("DeleteCita Error: " + ex.ToString());
                response.TypeOfResponse = TypeOfResponse.ErrorResponse;
                response.Message = "Excepction " + ex.Message;
            }
            return response;
        }

        public Response Cancel(Cita_Model appointment, DateTime date)
        {
            Response response = new Response();
            try
            {
                Citas cita = db.Citas.Where(x => x.uId == appointment.uId).FirstOrDefault();
                cita.razon = appointment.razon;
                cita.status_id = 2;
                cita.cancel_date = date;
                db.SaveChanges();
                appointment.precio = cita.precio;
                response.Data = appointment;
                response.TypeOfResponse = TypeOfResponse.OK;
                response.Message = "La cita se ha eliminado correctamente";
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("DeleteCita Error: " + ex.ToString());
                response.TypeOfResponse = TypeOfResponse.ErrorResponse;
                response.Message = "Excepction " + ex.Message;
            }
            return response;
        }

        public Response Confirm(Guid uId, Offices_model office)
        {
            Response response = new Response { Message = "Success", Data = null, TypeOfResponse = TypeOfResponse.OK };
            try
            {
                VL_CitasEntities db = new VL_CitasEntities();
                Citas cita = db.Citas.Find(uId);
                cita.status_id = 4;
                cita.confirm_date = office.configuration.Now;
                db.SaveChanges();

                response.Message = "La cita se ha re agendado correctamente";
                response.TypeOfResponse = TypeOfResponse.OK;

                var userInOffice = db.Offices_Users.Where(x => x.office_uid == office.uId && x.user_uid == cita.doctor_uid).FirstOrDefault();
                userInOffice.medical_appointment_duration = userInOffice.medical_appointment_duration == null ? 30 : userInOffice.medical_appointment_duration;
                double time_prom = Convert.ToDouble(userInOffice.medical_appointment_duration);
                response.Data = new CitasByDoctor
                {
                    id = cita.uId,
                    title = cita.title,
                    start_date = cita.cita_date,
                    start = cita.cita_date.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end_date = cita.cita_date.Value.AddMinutes(time_prom),
                    end = cita.cita_date.Value.AddMinutes(time_prom).ToString("yyyy-MM-ddTHH:mm:ss"),
                    durationEditable = false,
                    status_id = cita.status_id,
                    color = cita.Status_Citas.color,
                    consultory_uid = (Guid)cita.consultory_uId,
                    precio = cita.precio,
                    office_uId = office.uId,
                    doctor_uid = cita.doctor_uid,
                    doctor_name = cita.Users.first_name + " " + cita.Users.last_name
                };
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("ConfirmarCita Error: " + ex.ToString());
                response.TypeOfResponse = TypeOfResponse.ErrorResponse;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response Complete(Cita_Model appointment, DateTime date)
        {
            Response response = new Response();
            try
            {
                var receta_html = System.Net.WebUtility.HtmlDecode(appointment.receta);
                var motivo = System.Net.WebUtility.HtmlDecode(appointment.motivo);
                var diagnosis = System.Net.WebUtility.HtmlDecode(appointment.diagnostico);
                var obervaciones = System.Net.WebUtility.HtmlDecode(appointment.observaciones);
                var observaciones_assistant = System.Net.WebUtility.HtmlDecode(appointment.observaciones_asistente);
                VL_CitasEntities db = new VL_CitasEntities();
                Citas ecita = db.Citas.Where(x => x.uId == appointment.uId).FirstOrDefault();
                ecita.precio = appointment.precio;
                ecita.observaciones = obervaciones;
                ecita.observaciones_asistente = observaciones_assistant;
                ecita.status_id = 3;
                ecita.peso = appointment.peso;
                ecita.temperatura = appointment.temperatura;
                ecita.presion = appointment.presion;
                ecita.altura = appointment.altura;
                ecita.ritmo_cardiaco = appointment.ritmo_cardiaco;
                ecita.glucosa = appointment.glucosa;
                ecita.receta = receta_html;
                ecita.complete_date = date;
                ecita.motivo = motivo;
                ecita.diagnostico = diagnosis;
                db.SaveChanges();
                response.TypeOfResponse = TypeOfResponse.OK;
                response.Message = "La cita se ha completado correctamente";
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("CompleteCita Error: " + ex.ToString());
                response.TypeOfResponse = TypeOfResponse.ErrorResponse;
                response.Message = "Excepction " + ex;
            }
            return response;
        }

        public Response GetInformation(Guid uId)
        {
            Response response = new Response();
            try
            {
                Citas cita = db.Citas.Where(x => x.uId == uId).FirstOrDefault();
                Cita_Model data = new Cita_Model(cita);
                response.Data = data;
                response.TypeOfResponse = TypeOfResponse.OK;
                response.Message = "La información se ha encontrado correctamente";
            }
             catch (Exception ex)
            {
                Common.Set_Log_Errors("CompleteCita Error: " + ex.ToString());
                response.TypeOfResponse = TypeOfResponse.ErrorResponse;
                response.Message = "Excepction " + ex;
            }
            return response;
        }

        public int GetCitasOfTheMonthByConsultory(Guid uId, DateTime now)
        {
            DateTime start = new DateTime(now.Year, now.Month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            return GetTotalCitasByConsultory(uId, start, end);
        }

        public int GetCitasOfTodayByConsultory(Guid uId, DateTime now)
        {
            DateTime start = now.Date;
            DateTime end = now.Date.AddDays(1);
            return GetTotalCitasByConsultory(uId, start, end);
        }

        public List<CitasByDoctor> GetCitasByConsultory(Guid uId, string pathInfo, Guid office_uid)
        {
            try
            {
                List<CitasByDoctor> list = db.GetCitasByConsultory(uId).Select(x => new CitasByDoctor()
                {
                    id = x.id,
                    title = x.title,
                    start_date = x.start,
                    start = x.start.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end_date = x.end,
                    end = x.end.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                    precio = x.precio,
                    color = x.color,
                    durationEditable = false,
                    status_id = x.status_id,
                    url = pathInfo + "/Citas/Detalle?cita_uid=" + x.id,
                    consultory_uid = uId,
                    office_uId = office_uid,
                    doctor_uid = x.doctor_uid,
                    doctor_name = x.doctor_name
                }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("ValidateUser Error:" + ex.ToString());
                return null;
            }
        }


        public Response SendAlerts(string path)
        {
            Response res = new Response { TypeOfResponse = TypeOfResponse.OK, Message = "Success" };
            try
            {
                VL_CitasEntities db = new VL_CitasEntities();
                DateTime tomorrow = DateTime.Now.AddDays(1);
                DateTime start_date = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 0, 0, 1);
                DateTime end_date = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 23, 59, 59);
                List<Guid> citas_ids = db.Citas.Where(x => x.cita_date >= start_date && x.cita_date <= end_date && x.status_id == 1 && x.Paciente.email != null).Select(x => x.uId).ToList();
                ThreadPool.QueueUserWorkItem(sender => Common.sendNotificationEmails(citas_ids, path));
            }
             catch (Exception ex)
            {
                Common.Set_Log_Errors("Maintenance Controller - SendAlerts -> Error: " + ex.ToString());
                res.TypeOfResponse = TypeOfResponse.ErrorResponse;
                res.Message = ex.Message;
                res.Data = null;
            }
            return res;
        }

        public int GetTotalCitas(Guid doctor_uid, DateTime start, DateTime end)
        {
            int total = 0;
            try
            {
                total = db.Citas.Where(x => x.doctor_uid == doctor_uid && x.cita_date>= start && x.cita_date <= end).Count();
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("GetTotalCitas Error:" + ex.ToString());
            }
            return total;
        }

        public int GetTotalCitasByConsultory(Guid uid, DateTime start, DateTime end)
        {
            int total = 0;
            try
            {
                total = db.Citas.Where(x => x.consultory_uId == uid && x.cita_date >= start && x.cita_date <= end).Count();
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("GetTotalCitas Error:" + ex.ToString());
            }
            return total;
        }

        public int GetCitasOfToday(Guid doctor_uid, DateTime now)
        {
            DateTime start = now.Date;
            DateTime end = now.Date.AddDays(1);
            return GetTotalCitas(doctor_uid, start, end);
        }

        public int GetCitasOfTheMonth(Guid doctor_uid, DateTime now)
        {
            DateTime start = new DateTime(now.Year, now.Month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            return GetTotalCitas(doctor_uid, start, end);
        }

        public List<CitasByDoctor> GetCitas(Guid user_uId, string pathUrl)
        {
            try
            {
                List<CitasByDoctor> list = db.GetCitasByDoctor(user_uId).Select(x => new CitasByDoctor()
                {
                    id = x.id,
                    title = x.title,
                    start_date = x.start,
                    start = x.start.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end_date = x.end,
                    end = x.end.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                    precio = x.precio,
                    color = x.color,
                    durationEditable = false,
                    status_id = x.status_id,
                    url = pathUrl + "/Citas/Detalle?cita_uid=" + x.id,
                    consultory_uid = x.consultory_uid,
                    office_uId = x.office_uId,
                    doctor_uid = user_uId,
                    doctor_name = x.doctor_name
                }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("ValidateUser Error:" + ex.ToString());
                return null;
            }
        }
    }

   

    
}