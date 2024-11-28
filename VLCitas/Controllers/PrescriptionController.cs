using VLCitas.Models;
using VLCitas.DataLayer.PrescriptionsRepository;
using System.Web.Mvc;
using VLCitas.DataLayer;
using VLCitas.DataLayer.UsersRepository;
using VLCitas.DataLayer.OfficesRepository;

namespace VLCitas.Controllers
{
    public class PrescriptionController : MyController
    {
        private PrescriptionRepository prescriptionRepo;
        public PrescriptionController()
        {
            prescriptionRepo = new PrescriptionRepository();
        }

        [HttpPost]
        public ActionResult New(Patient_Prescriptions_Model data)
        {
            Users user = (Users)Session["user"];
            Offices_model office = (Offices_model)Session["office"];
            data.user_uid = user.uId;
            Prescriptions response = prescriptionRepo.GetTemplate(data, office.uId);
            ViewBag.Title = "Basic";
            if (response != null)
                ViewBag.Title = response.name;

            return RedirectToAction(ViewBag.Title, new DoctorPrescription(user, office));
        }
    }
}