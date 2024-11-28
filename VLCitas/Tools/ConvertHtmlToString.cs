using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiQPdf;
using SureSpotMVC.Models;
using System.Net.Mime;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using VLCitas.DataLayer.CommonRepository;

namespace HIQPDF_
{
    public class ConvertHtmlToString
    {

        public String TituloSistema { get; set; }
        public String PiePagina { get; set; }
        public String NombreCompleto { get; set; }

        public string RenderRazorViewToString(string viewName, object model, ControllerContext ctx)
        {
            ctx.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ctx, viewName);
                var viewContext = new ViewContext(ctx, viewResult.View, ctx.Controller.ViewData, ctx.Controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ctx, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public static string SaveDirectPDF(string url, string path)
        {
            string result = "Error";
            try
            {
                ConvertHtmlToString GenerarPDF = new ConvertHtmlToString
                {
                    NombreCompleto = "SureSpot Inc."
                };
                RPDFModel pdfsetting = new RPDFModel
                {
                    hasfooter = false,
                    hasHeader = false,
                    timeforwait = 3,
                    Bottom = 0,
                    Top = 0,
                    Right = 0,
                    Left = 0,
                    PDFsize = new PdfPageSize((float)201.6, (float)297.6)
                };
                result = GenerarPDF.SavePDF(url, pdfsetting, path);
            }
            catch (Exception ex)
            {
                pdfError Error = new pdfError();
                result = ex.Message;
                Error.Status = ex;
                //Common.Set_Log_Errors("save_DirectPDF -> ERROR: " + ex.ToString());
            }
            return result;
        }

        public byte[] GenerarPDF(string viewName, object model, ControllerContext ctx, bool hasHeader)
        {
            String View = RenderRazorViewToString(viewName, model, ctx);
            HiQPdf.HtmlToPdf docpdf = new HtmlToPdf();

            docpdf.Document.Margins.Bottom = 20;
            docpdf.Document.Margins.Top = 20;
            docpdf.Document.Margins.Left = 20;
            docpdf.Document.Margins.Right = 20;
            //docpdf.SerialNumber = "s/va4uPX-1f/a0cHS-wcqFnYOT-gpOHk4qL-h5OAgp2C-gZ2KioqK";
            if (hasHeader)
                SetFooter(docpdf.Document);
            byte[] archivo = docpdf.ConvertHtmlToPdfDocument(View, "").WriteToMemory();
            return archivo;
        }

        public byte[] New_Generate_PDF(string viewName, object model, ControllerContext ctx, RPDFModel pdfsetting, int trigertype = 1)
        {
            String View = RenderRazorViewToString(viewName, model, ctx);
            HiQPdf.HtmlToPdf docpdf = settings_pdf(viewName, model, ctx, pdfsetting, trigertype);

            byte[] pdfBuffer = null;
            // convert HTML code
            string htmlCode = View;
            string baseUrl = "";

            // convert HTML code to a PDF memory buffer
            pdfBuffer = docpdf.ConvertHtmlToMemory(htmlCode, baseUrl);

            //byte[] archivo = docpdf.ConvertHtmlToPdfDocument(View, "").WriteToMemory();
            return pdfBuffer;
        }
        public string New_Download_PDF(string viewName, object model, ControllerContext ctx, RPDFModel pdfsetting, string name_pdf, int trigertype = 1)
        {
            String View = RenderRazorViewToString(viewName, model, ctx);
            HiQPdf.HtmlToPdf docpdf = settings_pdf(viewName, model, ctx, pdfsetting, trigertype);
            string date = DateTime.Now.ToString("yyyy");
            string paths = HttpContext.Current.Server.MapPath("/PDF/Sendbyemail/");
            name_pdf = name_pdf != null && name_pdf != "" ? name_pdf : viewName + " "+ DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss").Replace(":","");

            string PDFPath = paths + name_pdf + ".pdf";

            if (File.Exists(PDFPath))
            {
                File.Delete(PDFPath);

            }

            docpdf.ConvertHtmlToFile(View, "", PDFPath);
            return PDFPath;
        }
        private HiQPdf.HtmlToPdf settings_pdf(string viewName, object model, ControllerContext ctx, RPDFModel pdfsetting, int trigertype = 1)
        {

            HiQPdf.HtmlToPdf docpdf = new HtmlToPdf();
            if (trigertype == 1)
                docpdf.TriggerMode = ConversionTriggerMode.Auto;
            if (trigertype == 2)
            {
                docpdf.TriggerMode = ConversionTriggerMode.WaitTime;
                docpdf.WaitBeforeConvert = pdfsetting.timeforwait;
            }
            if (trigertype == 3)
                docpdf.TriggerMode = ConversionTriggerMode.Manual;

            docpdf.Document.Margins.Bottom = pdfsetting.Bottom;
            docpdf.Document.Margins.Top = pdfsetting.Top;
            docpdf.Document.Margins.Left = pdfsetting.Left;
            docpdf.Document.Margins.Right = pdfsetting.Right;
            docpdf.Document.PageOrientation = pdfsetting.PdfOrientation;

            if (pdfsetting.hasfooter)
                Set_Footer(docpdf.Document, pdfsetting.footer);
            if (pdfsetting.hasHeader)
                Set_Header(docpdf.Document, pdfsetting.header);
            //docpdf.SerialNumber = "s/va4uPX-1f/a0cHS-wcqFnYOT-gpOHk4qL-h5OAgp2C-gZ2KioqK";
            return docpdf;
        }
        public byte[] GeneratePDF(string url)
        {
            HiQPdf.HtmlToPdf docpdf = new HtmlToPdf();
            object x = new object();

            docpdf.Document.Margins.Bottom = 20;
            docpdf.Document.Margins.Top = 20;
            docpdf.Document.Margins.Left = 20;
            docpdf.Document.Margins.Right = 20;

            //docpdf.ConvertUrlToFile(url,"Example" ); 
            byte[] file = docpdf.ConvertUrlToPdfDocument(url).WriteToMemory();
            // byte[] pdfBuffer = null;
            // pdfBuffer = docpdf.ConvertUrlToMemory(url);
            // return pdfBuffer;
            //byte[] file = docpdf.ConvertUrlToMemory(url);
            return file;
        }
        public byte[] Generate_PDF(string url, RPDFModel pdfsetting)
        {
            HiQPdf.HtmlToPdf docpdf = new HtmlToPdf();
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            docpdf.TriggerMode = ConversionTriggerMode.WaitTime;
            docpdf.WaitBeforeConvert = pdfsetting.timeforwait;

            docpdf.Document.Margins.Bottom = pdfsetting.Bottom;
            docpdf.Document.Margins.Top = pdfsetting.Top;
            docpdf.Document.Margins.Left = pdfsetting.Left;
            docpdf.Document.Margins.Right = pdfsetting.Right;
            docpdf.Document.PageOrientation = pdfsetting.PdfOrientation;

            //docpdf.ConvertUrlToFile(url,"Example" ); 
            if (pdfsetting.hasfooter)
                Set_Footer(docpdf.Document, pdfsetting.footer);
            if (pdfsetting.hasHeader)
                Set_Header(docpdf.Document, pdfsetting.header);
            //docpdf.SerialNumber = "s/va4uPX-1f/a0cHS-wcqFnYOT-gpOHk4qL-h5OAgp2C-gZ2KioqK";

            byte[] file = docpdf.ConvertUrlToPdfDocument(url).WriteToMemory();
            // byte[] pdfBuffer = null;
            // pdfBuffer = docpdf.ConvertUrlToMemory(url);
            // return pdfBuffer;
            //byte[] file = docpdf.ConvertUrlToMemory(url);
            return file;
        }
        public byte[] Generate_PDF_b64(string url, RPDFModel pdfsetting)
        {
            HiQPdf.HtmlToPdf docpdf = new HtmlToPdf();
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            docpdf.TriggerMode = ConversionTriggerMode.WaitTime;
            docpdf.WaitBeforeConvert = pdfsetting.timeforwait;

            docpdf.Document.Margins.Bottom = pdfsetting.Bottom;
            docpdf.Document.Margins.Top = pdfsetting.Top;
            docpdf.Document.Margins.Left = pdfsetting.Left;
            docpdf.Document.Margins.Right = pdfsetting.Right;
            docpdf.Document.PageOrientation = pdfsetting.PdfOrientation;

            //docpdf.ConvertUrlToFile(url,"Example" ); 
            if (pdfsetting.hasfooter)
                Set_Footer(docpdf.Document, pdfsetting.footer);
            if (pdfsetting.hasHeader)
                Set_Header(docpdf.Document, pdfsetting.header);
            //docpdf.SerialNumber = "s/va4uPX-1f/a0cHS-wcqFnYOT-gpOHk4qL-h5OAgp2C-gZ2KioqK";

            byte[] file = docpdf.ConvertUrlToPdfDocument(url).WriteToMemory();
            // byte[] pdfBuffer = null;
            // pdfBuffer = docpdf.ConvertUrlToMemory(url);
            // return pdfBuffer;
            //byte[] file = docpdf.ConvertUrlToMemory(url);
            return file;
        }
        public string sendbyemailGenerate_PDF(string url, RPDFModel pdfsetting, string path)
        {
            HiQPdf.HtmlToPdf docpdf = new HtmlToPdf();
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            docpdf.TriggerMode = ConversionTriggerMode.WaitTime;
            docpdf.WaitBeforeConvert = pdfsetting.timeforwait;

            docpdf.Document.Margins.Bottom = pdfsetting.Bottom;
            docpdf.Document.Margins.Top = pdfsetting.Top;
            docpdf.Document.Margins.Left = pdfsetting.Left;
            docpdf.Document.Margins.Right = pdfsetting.Right;
            docpdf.Document.PageOrientation = pdfsetting.PdfOrientation;

            //docpdf.ConvertUrlToFile(url,"Example" ); 
            if (pdfsetting.hasfooter)
                Set_Footer(docpdf.Document, pdfsetting.footer);
            if (pdfsetting.hasHeader)
                Set_Header(docpdf.Document, pdfsetting.header);
            //docpdf.SerialNumber = "s/va4uPX-1f/a0cHS-wcqFnYOT-gpOHk4qL-h5OAgp2C-gZ2KioqK";

            //var PDFPath = HttpContext.Current.Server.MapPath("~/Pdf/" + path + ".pdf");
            //var PDFPath = "C:\\Users\\Luisde\\Source\\Repos\\SSWebService\\SureSpot\\SureSpotWebAPI\\pdf\\validations\\" + path + ".pdf";
            //string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string date = DateTime.Now.ToString("yyyy");
            var pathws = Settings.Url +"\\pdf\\validations\\";
            string paths = System.IO.Path.GetDirectoryName(pathws);

            var PDFPath = paths + path + ".pdf";

            //if (!Directory.Exists(path_ws))
            //{
            //    Directory.CreateDirectory(path_ws);
            //}
            //if (!Directory.Exists(pathws))
            //{
            //    Directory.CreateDirectory(pathws);
            //}
            if (File.Exists(PDFPath))
            {
                File.Delete(PDFPath);

            }
            ////docpdf.ConvertUrlToFile(url, PDFPath);

            //docpdf.ConvertUrlToPdfDocument(url).WriteToFile(PDFPath);
            docpdf.ConvertUrlToFile(url, PDFPath);
            //File(item, "application/pdf", "SurespotError" + ".pdf");

            // item..WriteToFile(PDFPath);

            // byte[] pdfBuffer = null;
            // pdfBuffer = docpdf.ConvertUrlToMemory(url);
            // return pdfBuffer;
            //byte[] file = docpdf.ConvertUrlToMemory(url);
            return PDFPath;
        }
        public byte[] GenerarPDF_Blanco(string viewName, object model, ControllerContext ctx)
        {
            String View = RenderRazorViewToString(viewName, model, ctx);
            HiQPdf.HtmlToPdf docpdf = new HtmlToPdf();

            docpdf.Document.Margins.Bottom = 0;
            docpdf.Document.Margins.Top = 0;
            docpdf.Document.Margins.Left = 0;
            docpdf.Document.Margins.Right = 0;
            //docpdf.SerialNumber = "s/va4uPX-1f/a0cHS-wcqFnYOT-gpOHk4qL-h5OAgp2C-gZ2KioqK";
            byte[] archivo = docpdf.ConvertHtmlToPdfDocument(View, "").WriteToMemory();
            return archivo;
        }





        private void Set_Header(PdfDocumentControl htmlToPdfDocument, pdfheader header)
        {
            htmlToPdfDocument.Header.Enabled = true;
            htmlToPdfDocument.Header.Height = 50;
            if (header.transparent)
                htmlToPdfDocument.Header.BackgroundColor = System.Drawing.Color.White;
            else
                System.Drawing.Color.FromArgb(header.BG_alpha, header.BG_red, header.BG_green, header.BG_blue);

            float pdfPageWidth = htmlToPdfDocument.PageOrientation == PdfPageOrientation.Portrait ?
                                htmlToPdfDocument.PageSize.Width : htmlToPdfDocument.PageSize.Height;
            float headerWidth = pdfPageWidth - htmlToPdfDocument.Margins.Left
                        - htmlToPdfDocument.Margins.Right;
            float headerHeight = htmlToPdfDocument.Header.Height;

            System.Drawing.Font headertextFont =
              new System.Drawing.Font(
              new System.Drawing.FontFamily("Times New Roman"),
              14, System.Drawing.GraphicsUnit.Point);
            PdfText headerText = new PdfText(0, -2, header.header, headertextFont);
            headerText.HorizontalAlign = PdfTextHAlign.Center;
            headerText.EmbedSystemFont = true;
            headerText.ForeColor = System.Drawing.Color.FromArgb(header.BG_alpha, header.BG_red, header.BG_green, header.BG_blue);
            //headerText.ForeColor = System.Drawing.Color.Black;
            htmlToPdfDocument.Header.Layout(headerText);

            headertextFont =
              new System.Drawing.Font(
              new System.Drawing.FontFamily("Times New Roman"),
              10, System.Drawing.GraphicsUnit.Point);
            headerText = new PdfText(0, 10, header.title, headertextFont);
            headerText.HorizontalAlign = PdfTextHAlign.Center;
            headerText.EmbedSystemFont = true;
            headerText.ForeColor = System.Drawing.Color.FromArgb(header.BG_alpha, header.BG_red, header.BG_green, header.BG_blue);
            //headerText.ForeColor = System.Drawing.Color.Black;
            htmlToPdfDocument.Header.Layout(headerText);

            headertextFont =
              new System.Drawing.Font(
              new System.Drawing.FontFamily("Times New Roman"),
              10, System.Drawing.GraphicsUnit.Point);
            headerText = new PdfText(0, 19, header.subtittle, headertextFont);
            headerText.HorizontalAlign = PdfTextHAlign.Center;
            headerText.EmbedSystemFont = true;
            headerText.ForeColor = System.Drawing.Color.FromArgb(header.BG_alpha, header.BG_red, header.BG_green, header.BG_blue);
            //headerText.ForeColor = System.Drawing.Color.Black;
            htmlToPdfDocument.Header.Layout(headerText);


            header.Logo = HttpContext.Current.Server.MapPath("/Content/images/logo.png");
            PdfImage logoHeaderImage = new PdfImage();
            if (htmlToPdfDocument.PageOrientation == PdfPageOrientation.Portrait)
                logoHeaderImage = new PdfImage(450, 0, 75, System.Drawing.Image.FromFile(header.Logo));
            else
                logoHeaderImage = new PdfImage(700, 0, 75, System.Drawing.Image.FromFile(header.Logo));


            htmlToPdfDocument.Header.Layout(logoHeaderImage);


        }
        private void Set_Footer(PdfDocumentControl htmlToPdfDocument, pdffooter footer)
        {
            htmlToPdfDocument.Footer.Enabled = true;
            htmlToPdfDocument.Footer.Height = 15;
            htmlToPdfDocument.Footer.BackgroundColor = System.Drawing.Color.FromArgb(footer.BG_alpha, footer.BG_red, footer.BG_green, footer.BG_blue);

            float pdfPageWidth = htmlToPdfDocument.PageOrientation == PdfPageOrientation.Portrait ?
                        htmlToPdfDocument.PageSize.Width : htmlToPdfDocument.PageSize.Height;

            float footerWidth = pdfPageWidth - htmlToPdfDocument.Margins.Left -
                        htmlToPdfDocument.Margins.Right;
            float footerHeight = htmlToPdfDocument.Footer.Height;

            //PdfHtml footerHtml = new PdfHtml(5, 0, footer.footer, null);
            //footerHtml.FitDestHeight = true;
            //footerHtml.ForeColor = System.Drawing.Color.White;
            //htmlToPdfDocument.Footer.Layout(footerHtml);

            System.Drawing.Font footertextFont =
               new System.Drawing.Font(
               new System.Drawing.FontFamily("Times New Roman"),
               7, System.Drawing.GraphicsUnit.Point);
            PdfText footerText = new PdfText(5, 3, footer.leftfooter, footertextFont);
            footerText.HorizontalAlign = PdfTextHAlign.Left;
            footerText.EmbedSystemFont = true;
            footerText.ForeColor = System.Drawing.Color.White;
            htmlToPdfDocument.Footer.Layout(footerText);
            //footer.Logo = HttpContext.Current.Server.MapPath("~/Content/images/logo.png");
            //PdfImage logoHeaderImage = new PdfImage(40, 1, 25, System.Drawing.Image.FromFile(footer.Logo));
            //htmlToPdfDocument.Footer.Layout(logoHeaderImage);

            //System.Drawing.Font footertextFont =
            //   new System.Drawing.Font(
            //   new System.Drawing.FontFamily("Times New Roman"),
            //   7, System.Drawing.GraphicsUnit.Point);
            footerText = new PdfText(75, 3, footer.footer, footertextFont);
            footerText.HorizontalAlign = PdfTextHAlign.Left;
            footerText.EmbedSystemFont = true;
            footerText.ForeColor = System.Drawing.Color.White;
            htmlToPdfDocument.Footer.Layout(footerText);

            System.Drawing.Font pageNumberingFont =
                new System.Drawing.Font(
                new System.Drawing.FontFamily("Times New Roman"),
                7, System.Drawing.GraphicsUnit.Point);
            PdfText pageNumberingText = new PdfText(footerWidth - 100, 3,
                            "Page {CrtPage} of {PageCount}", pageNumberingFont);
            pageNumberingText.HorizontalAlign = PdfTextHAlign.Center;
            pageNumberingText.EmbedSystemFont = true;
            pageNumberingText.ForeColor = System.Drawing.Color.White;
            htmlToPdfDocument.Footer.Layout(pageNumberingText);
        }
        private void SetFooter(PdfDocumentControl htmlToPdfDocument)
        {
            // enable footer display
            htmlToPdfDocument.Footer.Enabled = true;

            // set footer height
            htmlToPdfDocument.Footer.Height = 15;
            // set footer background color
            htmlToPdfDocument.Footer.BackgroundColor = System.Drawing.Color.WhiteSmoke;

            float pdfPageWidth = htmlToPdfDocument.PageOrientation == PdfPageOrientation.Portrait ?
                    htmlToPdfDocument.PageSize.Width : htmlToPdfDocument.PageSize.Height;

            float footerWidth = pdfPageWidth - htmlToPdfDocument.Margins.Left -
                        htmlToPdfDocument.Margins.Right;
            float footerHeight = htmlToPdfDocument.Footer.Height;

            // layout HTML in footer
            if (String.IsNullOrEmpty(TituloSistema)) TituloSistema = "Volkano Labs";
            if (String.IsNullOrEmpty(PiePagina)) PiePagina = "{0} {1} {2}";
            if (String.IsNullOrEmpty(NombreCompleto)) NombreCompleto = "MAS";
            PdfHtml footerHtml = new PdfHtml(5, 0,
                    String.Format(PiePagina, TituloSistema, NombreCompleto, DateTime.Now), null);
            footerHtml.FitDestHeight = true;
            htmlToPdfDocument.Footer.Layout(footerHtml);

            // add page numbering
            System.Drawing.Font pageNumberingFont =
                            new System.Drawing.Font(
                            new System.Drawing.FontFamily("Times New Roman"),
                            7, System.Drawing.GraphicsUnit.Point);
            PdfText pageNumberingText = new PdfText(footerWidth - 100, 3,
                            "Page {CrtPage} of {PageCount}", pageNumberingFont);
            pageNumberingText.HorizontalAlign = PdfTextHAlign.Center;
            pageNumberingText.EmbedSystemFont = true;
            pageNumberingText.ForeColor = System.Drawing.Color.Gray;
            htmlToPdfDocument.Footer.Layout(pageNumberingText);
        }

        public static string SendEmail(string email, string subject, string body, AlternateView view = null, List<string> attachmentFilenames = null)
        {
            string FromEmail = Settings.Email;
            string password = Settings.Password;

            AlternateView avHtml = null;
            string htmlBody = "<html><body>" + body + "<br><br>If you have any questions or encounter any problems, please contact <b>support@surespot.com<b>." +
                              "<br><br><img style=\"width: 15 %;\" alt=\"Surespot Inc.\" src =\"cid:surespot.logo\"></body></html>";
            avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            string fileLogo = HttpContext.Current.Server.MapPath("~/Content/images/surespot-logo-b.png");
            LinkedResource logo = new LinkedResource(fileLogo, MediaTypeNames.Image.Jpeg);
            logo.ContentId = "surespot.logo";
            avHtml.LinkedResources.Add(logo);

            MailMessage m = new MailMessage();
            m.From = new MailAddress(FromEmail);
            if (email.Contains(";"))
            {
                foreach (var address in email.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    m.To.Add(address);
                }
            }
            else
            {
                m.To.Add(email);
            }

            //m.To.Add(new MailAddress(email));
            m.Subject = subject;
            if (view == null)
            {
                m.AlternateViews.Add(avHtml);
            }
            else
            {
                m.AlternateViews.Add(view);
            }

            if (attachmentFilenames != null)
            {
                foreach (var attachmentFilename in attachmentFilenames)
                {
                    Attachment attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = attachment.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(attachmentFilename);
                    disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
                    disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
                    disposition.FileName = Path.GetFileName(attachmentFilename);
                    disposition.Size = new FileInfo(attachmentFilename).Length;
                    disposition.DispositionType = DispositionTypeNames.Attachment;
                    m.Attachments.Add(attachment);
                }
            }

            m.IsBodyHtml = true;
            SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com", 587);
            clienteSmtp.Credentials = new NetworkCredential(FromEmail, password);
            clienteSmtp.EnableSsl = true;

            try
            {

                clienteSmtp.Send(m);
                m.AlternateViews.Dispose();
                return "OK";
            }
            catch (Exception ex)
            {
                m.AlternateViews.Dispose();
                //Set_Log_Errors("SendEmail Error: " + ex.ToString());
                return "SendEmail Error: " + ex.Message + " InnerException:" + ex.InnerException;
            }

        }

        public string New_Save_PDF(string viewName, object model, ControllerContext ctx, RPDFModel pdfsetting, string path)
        {
            string PDFPath = "";
            String View = RenderRazorViewToString(viewName, model, ctx);
            HiQPdf.HtmlToPdf docpdf = new HtmlToPdf();
            docpdf.TriggerMode = ConversionTriggerMode.Auto;
            //docpdf.WaitBeforeConvert = pdfsetting.timeforwait;

            docpdf.Document.Margins.Bottom = pdfsetting.Bottom;
            docpdf.Document.Margins.Top = pdfsetting.Top;
            docpdf.Document.Margins.Left = pdfsetting.Left;
            docpdf.Document.Margins.Right = pdfsetting.Right;
            docpdf.Document.PageOrientation = pdfsetting.PdfOrientation;

            if (pdfsetting.hasfooter)
                Set_Footer(docpdf.Document, pdfsetting.footer);
            if (pdfsetting.hasHeader)
                Set_Header(docpdf.Document, pdfsetting.header);
            //docpdf.SerialNumber = "s/va4uPX-1f/a0cHS-wcqFnYOT-gpOHk4qL-h5OAgp2C-gZ2KioqK";
            PDFPath = HttpContext.Current.Server.MapPath("~/PDF/Invoice/" + path + ".pdf");


            docpdf.ConvertHtmlToFile(View, "", PDFPath);
            return PDFPath;
        }


        public string New_Save_Invoice_PDF(string viewName, object model, ControllerContext ctx, bool hasHeader, string name)
        {
            string PDFPath = "";
            String View = RenderRazorViewToString(viewName, model, ctx);
            HiQPdf.HtmlToPdf docpdf = new HtmlToPdf();

            docpdf.Document.Margins.Bottom = 20;
            docpdf.Document.Margins.Top = 20;
            docpdf.Document.Margins.Left = 20;
            docpdf.Document.Margins.Right = 20;
            //docpdf.SerialNumber = "s/va4uPX-1f/a0cHS-wcqFnYOT-gpOHk4qL-h5OAgp2C-gZ2KioqK";
            if (hasHeader)
                SetFooter(docpdf.Document);

            PDFPath = HttpContext.Current.Server.MapPath("~/PDF/Invoice/" + name);


            docpdf.ConvertHtmlToFile(View, "", PDFPath);
            return PDFPath;
        }
        public string Save_New_PDF(string viewName, object model, ControllerContext ctx, bool hasHeader, string name, string path = "PDF/validations/")
        {
            name = name.Replace("/", "_");
            string PDFPath = "";
            String View = RenderRazorViewToString(viewName, model, ctx);
            HiQPdf.HtmlToPdf docpdf = new HtmlToPdf();

            docpdf.Document.Margins.Bottom = 20;
            docpdf.Document.Margins.Top = 20;
            docpdf.Document.Margins.Left = 20;
            docpdf.Document.Margins.Right = 20;
            //docpdf.SerialNumber = "s/va4uPX-1f/a0cHS-wcqFnYOT-gpOHk4qL-h5OAgp2C-gZ2KioqK";
            if (hasHeader)
                SetFooter(docpdf.Document);

            PDFPath = HttpContext.Current.Server.MapPath("~/" + path + name);


            docpdf.ConvertHtmlToFile(View, "", PDFPath);
            return PDFPath;
        }

        public string SavePDF(string url, RPDFModel pdfsetting, string path)
        {
            string PDFPath = "";
            try
            {
                HtmlToPdf docpdf = new HtmlToPdf();
                HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

                docpdf.TriggerMode = ConversionTriggerMode.WaitTime;
                docpdf.WaitBeforeConvert = pdfsetting.timeforwait;

                docpdf.Document.Margins.Bottom = pdfsetting.Bottom;
                docpdf.Document.Margins.Top = pdfsetting.Top;
                docpdf.Document.Margins.Left = pdfsetting.Left;
                docpdf.Document.Margins.Right = pdfsetting.Right;
                docpdf.Document.PageOrientation = pdfsetting.PdfOrientation;

                if (pdfsetting.hasfooter)
                    Set_Footer(docpdf.Document, pdfsetting.footer);
                if (pdfsetting.hasHeader)
                    Set_Header(docpdf.Document, pdfsetting.header);
                //docpdf.SerialNumber = "s/va4uPX-1f/a0cHS-wcqFnYOT-gpOHk4qL-h5OAgp2C-gZ2KioqK";

                Common.CreateDirectory("Pdf");
                var folders = path.Split('/');
                string folder = "";
                for (int i = 0; i < folders.Length - 1; i++)
                {
                    folder += folders[i] + "/";
                    Common.CreateDirectory("Pdf/" + folder);
                }

                PDFPath = Settings.ServerPath + "Pdf/" + path;
                if (File.Exists(PDFPath))
                {
                    File.Delete(PDFPath);
                }
                docpdf.ConvertUrlToFile(url, PDFPath);
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("SavePDF Error: " + ex.ToString());
            }
           
            return PDFPath;
        }

        internal string htmlToString(string viewName, object model, ControllerContext ControllerContext)
        {
            // create a string writer to receive the HTML code
            StringWriter stringWriter = new StringWriter();
            // get the view to render
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext,
                      viewName, null);

            TempDataDictionary tempdata = new TempDataDictionary();
            tempdata.Add("ispdf", true);
            tempdata.Add("Url", Settings.Url);
            // create a context to render a view based on a model
            ViewContext viewContext = new ViewContext(
                ControllerContext,
                viewResult.View,
                new ViewDataDictionary(model),
                tempdata,
                stringWriter
            );
            // render the view to a HTML code
            viewResult.View.Render(viewContext, stringWriter);
            // return the HTML code
            return stringWriter.ToString();
        }

       
    }
}