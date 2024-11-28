using VLCitas.filters;
using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.ReportsRepository;
using HIQPDF_;
using SureSpotMVC.Models;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using VLCitas.DataLayer;
using VLCitas.DataLayer.OfficesRepository;
using VLCitas.DataLayer.Models;

namespace VLCitas.Controllers
{
    [DoctorFilter]
    public class ReportsController : MyController
    {
        private RPDFModel pdfsetting;
        private ConvertHtmlToString GeneratePDF { get; set; }
        public ReportsController()
        {
            if (this.pdfsetting == null) this.pdfsetting = new RPDFModel();
            if (this.GeneratePDF == null) this.GeneratePDF = new ConvertHtmlToString();

        }

        public ActionResult ReportError(string error)
        {
            ViewBag.error = error;
            return View();
        }
        // GET: Reports
        public ActionResult getReport(ReportsRepository settings)
        {
            Users userSession = (Users)Session["user"];
            Offices_model office = (Offices_model)Session["office"];
            settings.office_uId = office.uId;
            settings.doctor_uId = userSession.uId;
            settings.end_date = settings.end_date.AddHours(23).AddMinutes(59).AddSeconds(59);
            Session["ReportSettings"] = settings;

            switch (settings.type_report)
            {
                case TypeOfReport.Dashboard:
                    break;
                case TypeOfReport.SummaryReport:
                    Response summary = settings.getSummaryReport();
                    if (summary.TypeOfResponse == TypeOfResponse.OK)
                    {
                        SummaryReport model = (SummaryReport)summary.Data;
                        Session["temp"] = model;
                        return PartialView("SummaryReport", model);
                    }
                    else
                    {
                        return RedirectToAction("ReportError", summary.Message);
                    }
                case TypeOfReport.DatesReport:
                    break;
                default:
                    break;
            } 

            return View();
        }

        public ActionResult SummaryReport()
        {
            SummaryReport model = (SummaryReport)Session["temp"];
            ViewBag.ispdf = true;
            ViewBag.Url = Settings.Url;
            return View(model);
        }

        [HttpGet]
        public ActionResult getPdfReport()
        {
            pdf_settings_model Result = new pdf_settings_model();
            ReportsRepository settings = (ReportsRepository)Session["ReportSettings"];
            object item = new object();
            try
            {
                ViewBag.ispdf = true;
                ViewBag.Url = Settings.Url;
                Result = setPdfReportSettings(settings);

                byte[] PDF = GeneratePDF.New_Generate_PDF(Result.view, Result.data, this.ControllerContext, Result.pdfsetting);
                return File(PDF, "application/pdf", Result.pdf_name + ".pdf");

            }
            catch (Exception ex)
            {
                pdfError Error = new pdfError();
                Error.Status = ex;
                byte[] PDF = GeneratePDF.GenerarPDF("ErrorReport", Error, this.ControllerContext, true);
                return File(PDF, "application/pdf", "SurespotError" + ".pdf");
            }
        }

        private pdf_settings_model setPdfReportSettings(ReportsRepository setting)
        {
            pdf_settings_model Result = new pdf_settings_model();
            ReportsRepository reports = new ReportsRepository();
            string subtittle = "";
            Users user = (Users)Session["user"];
            Offices_model office = (Offices_model)Session["office"];
            try
            {
                subtittle = Resource.From + setting.start_date.ToString("yyyy/MM/dd hh:mm:ss tt") + Resource.To + setting.end_date.ToString("yyyy/MM/dd hh:mm:ss tt");

                switch (setting.type_report)
                {
                    case TypeOfReport.SummaryReport:
                        Result.data = setting.getSummaryReport().Data;
                        break;
                   
                    default:
                        break;
                }

                Result.view = setting.type_report.ToString();
                string pretty_title = Regex.Replace(Result.view, "(\\B[A-Z])", " $1");
                Result.pdf_name = Result.view + "_" + DateTime.Now.ToString("ddmmyyyyhhmmss");
                Result.pdfsetting = new RPDFModel(office.uId, 0, pretty_title, subtittle);
            }
            catch (Exception ex)
            {

            }
            return Result;
        }

    }
}