using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.PatientsRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace VLCitas.DataLayer
{
    public partial class Paciente
    {
        public Paciente(Patient_Model Model)
        {
            id = Model.id;
            nombre = Model.nombre;
            telefono = Model.telefono;
            fecha_nacimiento = Model.fecha_nacimiento;
            alergias = Model.alergias;
            cirugias = Model.cirugias;
            email = Model.email;
            sex_id = Model.sex_id;
            heredofamiliares = Model.heredofamiliares;
            antecedentes_patologicos = Model.antecedentes_patologicos;
            antecedentes_no_patologicos = Model.antecedentes_no_patologicos;
            created_date = Model.created_date == null? DateTime.Now: Model.created_date;
        }
    }
}