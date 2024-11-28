using VLCitas.filters;
using VLCitas.DataLayer.Models;
using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.PatientsRepository;
using System.Collections.Generic;
using System.Web.Mvc;
using VLCitas.DataLayer;
using VLCitas.DataLayer.OfficesRepository;

namespace VLCitas.Controllers
{
    public class PacientesController : MyController
    {
        private PatientRepositoy repoPatient;
        public PacientesController()
        {
            repoPatient = new PatientRepositoy();
        }
        // GET: Pacientes
        [DoctorFilter]
        public ActionResult Expedientes()
        {
            Patient_Model model = new Patient_Model();
            Users userSession = (Users)Session["user"];
            List<Paciente> res = repoPatient.RecentPaciente(userSession.uId);
            return View(res);
        }

        [DoctorFilter]
        public ActionResult FindPaciente(string keyword)
        {
            keyword = keyword.ToUpper().Trim();
            Patient_Model model = new Patient_Model();
            Users userSession = (Users)Session["user"];
            List<Paciente> res = repoPatient.FindPaciente(keyword, userSession.uId);
            return View(res);
        }

        [DoctorFilter]
        public ActionResult Detalle(int id)
        {
            Patient_Model model = new Patient_Model();
            Users userSession = (Users)Session["user"];
            if (!repoPatient.isAvailableDoctor(id, userSession.uId))
            {
                return RedirectToAction("Unauthorised", "Error");
            }
            Paciente paciente = repoPatient.FindPaciente(id);
            return View(paciente);
        }


        public JsonResult Nuevo(Patient_Model model)
        {
            Response response = new Response();
            if (Session["office"]!=null)
            {
                Offices_model office = (Offices_model)Session["office"];
                model.created_date = office.configuration.Now;
            }
            model = repoPatient.AddNewPatient(model);
            if (model.id>0)
                response.TypeOfResponse = TypeOfResponse.OK;
            response.Data = model;

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}