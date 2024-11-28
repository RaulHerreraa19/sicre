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
  
    public class Patient_Model
    {
        public Patient_Model(){}
        public Patient_Model(Paciente Model) {
            ParseModel(Model);
        }
        private void ParseModel(Paciente Model) {
            id = Model.id;
            nombre = Model.nombre+" "+ Model.apellidos;
            telefono = Model.telefono;
            fecha_nacimiento = Model.fecha_nacimiento;
            alergias = Model.alergias;
            cirugias = Model.cirugias;
            email = Model.email;
            sex_id = Model.sex_id;
            heredofamiliares = Model.heredofamiliares;
            antecedentes_patologicos = Model.antecedentes_patologicos;
            antecedentes_no_patologicos = Model.antecedentes_no_patologicos;
            created_date = Model.created_date;
        }

        public int id { get; set; }
        public string nombre { get; set; }
        //public string apellidos { get; set; }
        public string telefono { get; set; }
        public string curp { get; set; }
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }
        public string alergias { get; set; }
        public string cirugias { get; set; }
        public string email { get; set; }
        public Nullable<int> sex_id { get; set; }
        public string heredofamiliares { get; set; }
        public string antecedentes_patologicos { get; set; }
        public string antecedentes_no_patologicos { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
    }

}