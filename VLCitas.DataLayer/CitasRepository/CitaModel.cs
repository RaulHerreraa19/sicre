using VLCitas.DataLayer;
using VLCitas.DataLayer.CitasRepository;
using VLCitas.DataLayer.ConsultoriesRepository;
using VLCitas.DataLayer.PatientsRepository;
using VLCitas.DataLayer.UsersRepository;
using System;


namespace VLCitas.DataLayer.CitasRepository
{
    public class Cita_Model
    {
        public Cita_Model() { }
        public Cita_Model(Citas Model) { ParseModel(Model); }
        private void ParseModel(Citas Model) {
            uId = Model.uId;
            doctor_uid = Model.doctor_uid;
            consultory_uId = Model.consultory_uId;
            create_date = Model.create_date;
            cita_date = Model.cita_date;
            status_id = Model.status_id;
            Code = Model.Code;
            title = Model.title;
            telefono = Model.telefono;
            razon = Model.razon;
            precio = Model.precio;
            observaciones = Model.observaciones;
            observaciones_asistente = Model.observaciones_asistente;
            peso = Model.peso;
            temperatura = Model.temperatura;
            presion = Model.presion;
            receta = Model.receta;
            cancel_date = Model.cancel_date;
            complete_date = Model.complete_date;
            confirm_date = Model.confirm_date;
            doctor_uid = Model.doctor_uid;
            altura = Model.altura;
            observacion_doctor = Model.observacion_doctor;

            id_patient = Model.id_paciente;
            if (id_patient != null)
                patient = new Patient_Model(Model.Paciente);
            if (doctor_uid != null)
                doctor = new User_Model(Model.Users);
            if (consultory_uId != null)
                consultory = new Consultory_Model(Model.Consultory);
        }
        public System.Guid uId { get; set; }
        public Nullable<System.Guid> doctor_uid { get; set; }
        public Nullable<System.Guid> consultory_uId { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public Nullable<System.DateTime> cita_date { get; set; }
        public Nullable<int> status_id { get; set; }
        public string Code { get; set; }
        public string title { get; set; }
        public string telefono { get; set; }
        public string razon { get; set; }
        public string observaciones { get; set; }
        public string observaciones_asistente { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<double> peso { get; set; }
        public Nullable<double> temperatura { get; set; }
        public string presion { get; set; }
        public string receta { get; set; }
        public Nullable<System.DateTime> cancel_date { get; set; }
        public Nullable<System.DateTime> complete_date { get; set; }
        public Nullable<System.DateTime> confirm_date { get; set; }
        public Nullable<double> altura { get; set; }
        public string observacion_doctor { get; set; }
        public Nullable<double> glucosa { get; set; }
        public string motivo { get; set; }
        public string diagnostico { get; set; }
        public string ritmo_cardiaco { get; set; }

        public Nullable<int> id_patient { get; set; }
        public Patient_Model patient { get; set; }
        public User_Model doctor { get; set; }
        public Consultory_Model consultory { get; set; }
    }

}
namespace VLCitas.DataLayer { 

    public partial class Citas
    {
        public Citas(Cita_Model Model)
        {
            uId = Model.uId;
            doctor_uid = Model.doctor_uid;
            consultory_uId = Model.consultory_uId;
            create_date = Model.create_date;
            cita_date = Model.cita_date;
            status_id = Model.status_id;
            Code = Model.Code;
            title = Model.title;
            telefono = Model.telefono;
            razon = Model.razon;
            precio = Model.precio;
            observaciones = Model.observaciones;
            observaciones_asistente = Model.observaciones_asistente;
            peso = Model.peso;
            temperatura = Model.temperatura;
            presion = Model.presion;
            receta = Model.receta;
            cancel_date = Model.cancel_date;
            complete_date = Model.complete_date;
            confirm_date = Model.confirm_date;
            doctor_uid = Model.doctor_uid;
            id_paciente = Model.id_patient;
        }
    }
}