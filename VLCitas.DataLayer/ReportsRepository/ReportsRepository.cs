using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLCitas.DataLayer.ReportsRepository
{
    public class ReportsRepository
    {
        public Guid office_uId { get; set; }
        public Guid doctor_uId { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public TypeOfReport type_report { get; set; }
        public string view { get; set; }
        public string report_name { get; set; }

        public Response getSummaryReport()
        {
            Response res = new Response { TypeOfResponse = TypeOfResponse.OK, Message = "Success" };
            VL_CitasEntities db = new VL_CitasEntities();
            SummaryReport data = new SummaryReport();
            try {
                data.SummaryHeader = db.Reports_GetSummaryHeader(this.office_uId, this.doctor_uId, this.start_date, this.end_date).FirstOrDefault();
                data.ByStatus = db.Reports_GetCitasByStatus(this.office_uId, this.doctor_uId, this.start_date, this.end_date).ToList();
                data.Services = db.Reports_ServicesSummary(this.office_uId, this.doctor_uId, this.start_date, this.end_date).ToList();
                data.ByDays = db.Reports_GetCitasByDays(this.office_uId, this.doctor_uId, this.start_date, this.end_date).ToList();
                res.Data = data;
            }
            catch(Exception ex)
            {
                res.Message = ex.Message;
                res.TypeOfResponse = TypeOfResponse.ErrorResponse;
            }
            return res;
        }
    }

    public enum TypeOfReport
    {
        Dashboard = 0,
        SummaryReport = 1,
        DatesReport = 2
    }

    public class SummaryReport
    {
        public Reports_GetSummaryHeader_Result SummaryHeader { get; set; } 
        public List<Reports_GetCitasByStatus_Result> ByStatus { get; set; }
        public List<Reports_ServicesSummary_Result> Services { get; set; }
        public List<Reports_GetCitasByDays_Result> ByDays { get; set; }
    }
}