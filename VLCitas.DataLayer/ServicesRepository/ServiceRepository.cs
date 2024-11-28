using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLCitas.DataLayer.ServicesRepository
{
    public class ServiceRepository
    {
        public List<Servicios> GetUser(Guid user_uId)
        {
            List<Servicios> all_servicios = new List<Servicios>();
            VL_CitasEntities db = new VL_CitasEntities();
            try {
                all_servicios = db.Servicios.Where(x => x.user_uId == user_uId && x.activo == true).ToList();
            }
             catch (Exception ex)
            {
                Common.Set_Log_Errors("getByDepartment -> Error: " + ex.ToString());
            }
            return all_servicios;
        }

        public List<Servicios> GetAvailableServicesByUser(Guid user_uId, Guid cita_uId)
        {
            List<Servicios> ser = new List<Servicios>();
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
                List<Servicios> all = db.Servicios.Where(x => x.user_uId == user_uId && x.activo == true).ToList();
                List<Servicios> used = db.Citas.Where(x => x.uId == cita_uId).FirstOrDefault().Servicios.ToList();
                ser = all.Except(used).OrderBy(x=>x.nombre).ToList();
            }
             catch (Exception ex)
            {
                Common.Set_Log_Errors("getAvailableServicesByUsere -> Error: " + ex.ToString());
            }
            return ser;
        }

        public Servicios GetService(int service_id)
        {
            Servicios the_servicio = new Servicios();
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
                the_servicio = db.Servicios.Where(x => x.id == service_id && x.activo == true).FirstOrDefault();
            }
             catch (Exception ex)
            {
                Common.Set_Log_Errors("getService -> Error: " + ex.ToString());

            }
            return the_servicio;
        }
    }

}