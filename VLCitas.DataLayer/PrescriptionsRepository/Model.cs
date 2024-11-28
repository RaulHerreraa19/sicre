using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLCitas.DataLayer.PrescriptionsRepository
{
    public class Patient_Prescriptions_Model
    {
        public Patient_Prescriptions_Model() {}
        public Patient_Prescriptions_Model(Patient_Prescriptions Model) {
            ParseModel(Model);
        }

        private void ParseModel(Patient_Prescriptions Model) {
            uid = Model.uid;
            patient_id = Model.patient_id;
            date = Model.date;
            description = Model.description;
            user_uid = Model.user_uid;
        }

        public System.Guid uid { get; set; }
        public Nullable<int> patient_id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string description { get; set; }
        public Nullable<System.Guid> user_uid { get; set; }
    }
}