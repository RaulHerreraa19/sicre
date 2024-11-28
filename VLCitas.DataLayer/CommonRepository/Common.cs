using VLCitas.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace VLCitas.DataLayer.CommonRepository
{
    public class Common
    {
        public static void Set_Log_Errors(string sLog, int? number = null)
        {
            //ThreadPool.QueueUserWorkItem(sender => Set_Log(sLog, "errors"));
            string sFileName = number == null ? "errors" : "errors_" + number.ToString();
            Set_Log(sLog, sFileName);
        }

        public static void Set_Log_Details(string sLog, int? number = null)
        {
            //ThreadPool.QueueUserWorkItem(sender => Set_Log(sLog, "details"));
            string sFileName = number == null ? "details" : "details_" + number.ToString();
            Set_Log(sLog, sFileName);
        }

        private static void Set_Log(string sLog, string sFileName)
        {
            try
            {
                Console.WriteLine(sFileName+" " + sFileName);
                string sLogFile = null;
                string sText = null;
                string path = AppDomain.CurrentDomain.BaseDirectory;
                CreateDirectory(path + "log");

                sLogFile = path + "log/"+ sFileName+".log";
                //sLogFile = "C:/Transactions.log";

                if (!File.Exists(sLogFile))
                {
                    using (StreamWriter sw = new StreamWriter(File.Open(sLogFile, FileMode.Create)))
                    {
                        sw.WriteLine("Start------");
                    }
                }
                else
                {
                    FileInfo fi = new FileInfo(sLogFile);
                    if (fi.Length > 5000000)
                    {
                        String now = DateTime.Now.ToString("ddMMyyyy_HHmm");
                        String newFile = path + "log/" + sFileName + "_" + now + ".log";
                        File.Copy(sLogFile, newFile, true);
                        File.Delete(sLogFile);
                    }
                }
                //File.OpenWrite(sLogFile)

                using (StreamReader swRead = File.OpenText(sLogFile))
                {
                    sText = swRead.ReadToEnd();
                    DateTime dt = DateTime.Now;
                    sText = sText + Environment.NewLine + GetFullTime(dt) + ": " + sLog;
                }

                using (StreamWriter sw = new StreamWriter(File.Open(sLogFile, FileMode.Open, FileAccess.Write, FileShare.Read)))
                {
                    //sw.WriteLine(sLog)
                    sw.Write(sText);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Set_Log Error:" + ex.ToString());
            }
        }

        public static string GetFullTime(DateTime dt)
        {
            string amPm = dt.ToString("tt");
            return dt.ToString("M/dd/yyyy hh:mm:ss") + "." + dt.Millisecond.ToString() + " " + amPm;
        }

        public static void CreateDirectory(string Name)
        {
            try
            {
                if (!Directory.Exists(Name))
                {
                    Directory.CreateDirectory(Name);
                }
            }
            catch (Exception ex)
            {
                Set_Log_Errors("CreateDirectory -> ERROR:  " + ex.ToString());
            }
        }


        public static void sendNotificationEmails(List<Guid> citas_ids, string path)
        {
            try
            {
                VL_CitasEntities db = new VL_CitasEntities();
                var citas = db.Citas.Where(x => citas_ids.Contains(x.uId)).ToList();
                foreach (var item in citas)
                {
                    try {
                        string subject = "Recordatorio de cita - " + item.Consultory.alias;
                        
                        string Body = string.Format("Hola "+item.Paciente.nombre + ", te recordamos que tienes una cita agendada el dia de mañana "+ String.Format("{0:f}", item.cita_date)+ " hemos reservado nuestro espacio para ti "+
                            "por lo que te solicitamos nos confirmes tu asistencia a la cita o en su caso la canceles en el siguiente link http://"+path+"/citas/confirmar?uid="+item.uId.ToString());
                        SendEmail(item.Paciente.email, subject, Body, null, null);
                    }
                    catch {
                    }
                }
            }
            catch (Exception ex)
            {
                Set_Log_Errors("sendInvoicesByEmail -> ERROR: " + ex.ToString());
            }
        }

        //send emails
        public static Response SendEmail(string email, string subject, string body, AlternateView view = null, List<string> attachmentFilenames = null)
        {
            Response Response = new Response();
            MailMessage m = new MailMessage();
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory;
                AlternateView avHtml = null;
                string htmlBody = "<html><body>" + body + "<br><br>SIC - Sistema Integral de Citas" +
                                   "<br><br>Volkano Labs - Soluciones en Software" +
                                  "<br><img style=\"width: 15 %;\" alt=\"Volkano Labs\" src =\"cid:Volkano.logo\"></body></html>";
                avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
                string fileLogo = path + "/img/Volkano.png";
                LinkedResource logo = new LinkedResource(fileLogo, MediaTypeNames.Image.Jpeg)
                {
                    ContentId = "Volkano.logo"
                };
                avHtml.LinkedResources.Add(logo);


                m.From = new MailAddress(Settings.Email);
                if (email.Contains(";"))
                {
                    foreach (var address in email.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        m.To.Add(address);
                    }
                }
                else
                    m.To.Add(email);

                //m.To.Add(new MailAddress(email));
                m.Subject = subject;
                if (view == null)
                    m.AlternateViews.Add(avHtml);
                else
                    m.AlternateViews.Add(view);

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
                //SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com", 587);
                SmtpClient clienteSmtp = new SmtpClient("mail.ctsvolkano.com", 587)
                {
                    Credentials = new NetworkCredential(Settings.Email, Settings.Password),
                    EnableSsl = true
                };

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


                clienteSmtp.Send(m);
                m.Attachments.Dispose();
                Response.Message = "OK";
                Response.TypeOfResponse = TypeOfResponse.OK;
            }
            catch (Exception ex)
            {
                Set_Log_Errors("SendEmail -> Error:" + ex.ToString());
                m.Attachments.Dispose();
                Response.Message = "SendEmail Error: " + ex.Message + " InnerException:" + ex.InnerException;
                Response.TypeOfResponse = TypeOfResponse.ErrorResponse;
            }
            return Response;

        }

        internal static void notifyCitaCreated(string body, string department, string email)
        {
            try
            {
                string subject = "Recordatorio de cita - " + department;
                SendEmail(email, subject, body, null, null);
            }
            catch (Exception ex)
            {
                Set_Log_Errors("sendInvoicesByEmail -> ERROR: " + ex.ToString());
            }
        }
    }



    public enum SystemRoles
    {
        Administrador = 2013,
        Root = 2015,
        Doctor = 3019,
        Assistant = 3020,
        AutoAssistant = 3021
    }
    public class Response
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public TypeOfResponse TypeOfResponse { get; set; }

        public Response()
        {
            Data = null;
            Message = "Error";
            TypeOfResponse = TypeOfResponse.ErrorResponse;
        }
    }
}