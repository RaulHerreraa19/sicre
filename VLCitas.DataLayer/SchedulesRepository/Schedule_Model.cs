using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLCitas.DataLayer.SchedulesRepository
{
    public class Schedule_Model
    {
        public Schedule_Model() { }
        public Schedule_Model(Schedule Model) {
            ParseModel(Model);
        }
        private void ParseModel(Schedule Model) {
            id = Model.id;
            lunes = Model.lunes == null ? false : Model.lunes;
            martes = Model.martes == null ? false : Model.martes;
            miercoles = Model.miercoles == null ? false : Model.miercoles;
            jueves = Model.jueves == null ? false : Model.jueves;
            viernes = Model.viernes == null ? false : Model.viernes;
            sabado = Model.sabado == null ? false : Model.sabado;
            domingo = Model.lunes == null ? false : Model.domingo;
            start_hour = Model.start_hour;
            end_hour = Model.end_hour;
            status = Model.status;
            schedule_name = Model.schedule_name;
        }

        public int id { get; set; }
        public Nullable<bool> lunes { get; set; }
        public Nullable<bool> martes { get; set; }
        public Nullable<bool> miercoles { get; set; }
        public Nullable<bool> jueves { get; set; }
        public Nullable<bool> viernes { get; set; }
        public Nullable<bool> sabado { get; set; }
        public Nullable<bool> domingo { get; set; }
        public Nullable<System.TimeSpan> start_hour { get; set; }
        public Nullable<System.TimeSpan> end_hour { get; set; }
        public Nullable<int> status { get; set; }
        public string schedule_name { get; set; }
    }
}