using VLCitas.Models;
using VLCitas.DataLayer.Tools;
using HiQPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLCitas.DataLayer;

namespace SureSpotMVC.Models
{
    public class RPDFModel
    {
        public RPDFModel()
        {
            this.Bottom = 20;
            this.Top = 25;
            this.Left = 20;
            this.Right = 20;
            this.timeforwait = 10;
            this.hasHeader = true;
            this.hasfooter = true;
            this.footer = new pdffooter();
            this.header = new pdfheader();
            this.PdfOrientation = PdfPageOrientation.Portrait;
            this.PDFsize = PdfPageSize.A4;

        }
        public RPDFModel(Guid office_uId, int carpark_id, string title, string subtitle)
        {
            this.Bottom = 20;
            this.Top = 25;
            this.Left = 20;
            this.Right = 20;
            this.timeforwait = 10;
            this.hasHeader = true;
            this.hasfooter = true;
            this.footer = new pdffooter();
            this.header = new pdfheader(office_uId, carpark_id, title, subtitle);
            this.PdfOrientation = PdfPageOrientation.Portrait;
            this.PDFsize = PdfPageSize.A4;
        }

        public int Bottom { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int timeforwait { get; set; }
        public bool hasHeader { get; set; }
        public bool hasfooter { get; set; }
        public PdfPageSize PDFsize { get; set; }

        public PdfPageOrientation PdfOrientation { get; set; }

        public pdffooter footer { get; set; }
        public pdfheader header { get; set; }


    }

    public class pdffooter
    {
        public pdffooter()
        {
            // this.BackgroundColor ="#";
            this.Logo = "";
            this.footer = "Volkano Labs";
            this.BG_alpha = 1;
            this.BG_red = 131;
            this.BG_green = 70;
            this.BG_blue = 154;
            this.leftfooter = "MAS (Medical Appointment System)";


        }
        //public string BackgroundColor { get; set; }
        public string leftfooter { get; set; }

        public string footer { get; set; }
        public int BG_alpha { get; set; }
        public int BG_red { get; set; }
        public int BG_green { get; set; }
        public int BG_blue { get; set; }
        public string Logo { get; set; }


    }
    public class pdfheader
    {
        public pdfheader()
        {
            // this.BackgroundColor ="#";
            this.header = "Reporte ";
            this.title = "";
            this.subtittle = null;

            this.BG_alpha = 1;
            this.BG_red = 131;
            this.BG_green = 70;
            this.BG_blue = 154;
            this.transparent = true;
            this.textcolor = System.Drawing.Color.Purple;
        }
        public pdfheader(Guid officeUid, int carpark_id, string title, string subtitle)
        {
            this.header = new Repository<Offices>().GetByID(x => x.uId == officeUid).name;
            this.title = title;
            this.subtittle = subtitle;
            this.Logo = new Repository<Offices>().GetByID(x => x.uId == officeUid).image_url;
            this.BG_alpha = 1;
            this.BG_red = 131;
            this.BG_green = 70;
            this.BG_blue = 154;
            this.transparent = true;
            this.textcolor = System.Drawing.Color.Purple;
        }
        //public string BackgroundColor { get; set; }
        public bool transparent { get; set; }
        public string title { get; set; }
        public string subtittle { get; set; }
        public string Logo { get; set; }

        public string header { get; set; }
        public int BG_alpha { get; set; }
        public int BG_red { get; set; }
        public int BG_green { get; set; }
        public int BG_blue { get; set; }
        public System.Drawing.Color textcolor { get; set; }


    }
    public class pdfError
    {
        public Exception Status { get; set; }

    }
    public class pdfReport
    {
        public string data { set; get; }
        public string customer_id { set; get; }
        public string subtittle { set; get; }
        public string ReportName { set; get; }
        public string User { set; get; }
        public int time { get; set; }
    }
    public class pdfReport_balance
    {
        public int id { set; get; }
        public string subtittle { set; get; }
        public string ReportName { set; get; }
        public string User { set; get; }
    }
    #region pdf
    public class pdf_settings_model
    {
        public object data { get; set; }
        public string pdf_name { get; set; }
        public RPDFModel pdfsetting { get; set; }
        public string view { get; set; }
    }
    #endregion
}
