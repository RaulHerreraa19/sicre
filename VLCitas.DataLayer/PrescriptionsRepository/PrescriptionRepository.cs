using VLCitas.DataLayer.CommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLCitas.DataLayer.PrescriptionsRepository
{
    public class PrescriptionRepository
    {
        public Prescriptions GetTemplate(Patient_Prescriptions_Model data, Guid office_uid)
        {
            Prescriptions Prescription =null;
            try
            {
                VL_CitasEntities db = new VL_CitasEntities();
                Prescription = db.Offices_Users.Where(x => x.user_uid == data.user_uid && x.office_uid == office_uid).Select(x => x.Prescriptions).FirstOrDefault();
                if (Prescription==null)
                    Prescription = db.Prescriptions.Where(x => x.id == 1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.Set_Log_Errors("GetTemplate Error:" + ex.ToString());
            }
            return Prescription;
        }
    }
}