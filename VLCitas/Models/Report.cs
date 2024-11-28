using VLCitas.DataLayer.CommonRepository;
using System;
using System.Linq;
using VLCitas.DataLayer;

namespace VLCitas.Models
{

    public class Report
    {
        public Report()
        {
            this.Response = new Response();
        }
        private VL_CitasEntities db = new VL_CitasEntities();
        public System.Guid uId { get; set; }
        public Nullable<System.DateTime> start { get; set; }
        public Nullable<System.DateTime> fin { get; set; }
        public Nullable<int> id { get; set; }
        public Nullable<int> type_report { get; set; }
        public Nullable<int> canceladas { get; set; }
        public Nullable<int> agendadas { get; set; }
        public Nullable<int> confirmadas { get; set; }
        public Nullable<int> completadas { get; set; }
        public String nombre { get; set; }
        public Response Response { get; set; }
        public Object datatable { get; set; }
        public Object datapie { get; set; }
        public Object month_chart { get; set; }
        public Object total_square { get; set; }
        public Object datatable_service { get; set; }

        public Response reports(Report re)
        {
            Response response = new Response();
            if (re.id == 0)
            {
                //Report by todo, by departament.
                re.nombre = "ALL USERS";
                re.datatable = db.SPR_TotalCYGanancia(re.uId, re.start, re.fin, re.id).ToList();
                re.datapie = db.SPR_Ganancia_Mes(re.uId, re.start, re.fin, re.id).ToList();
                re.datatable_service = db.SPR_Servicio_Cantidad(re.uId, re.start, re.fin, re.id).ToList();
                response.Data = re;
                response.Message = "El reporte se ha creado correctamente";
                return response;
            }
            else
            {
                //Report by user only
                Paciente pac = db.Paciente.Where(x => x.id == re.id).FirstOrDefault();
                re.nombre = pac.nombre;
                re.datatable = db.SPR_TotalCYGanancia(re.uId, re.start, re.fin, re.id).ToList();
                re.datapie = db.SPR_Ganancia_Mes(re.uId, re.start, re.fin, re.id).ToList();
                re.datatable_service = db.SPR_Servicio_Cantidad(re.uId, re.start, re.fin, re.id).ToList();
                response.Data = re;
                response.Message = "El reporte se ha creado correctamente";
                return response;
            }
            
        }
    }
}