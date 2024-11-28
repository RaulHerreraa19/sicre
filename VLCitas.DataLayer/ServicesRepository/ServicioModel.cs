using VLCitas.DataLayer.CommonRepository;
using VLCitas.DataLayer.Models;
using VLCitas.DataLayer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLCitas.DataLayer.ServicesRepository
{
    public class Service_Model
    {
        public Service_Model() {}
        public Service_Model(Servicios Model) {}
        private void ParseModel(Servicios Model) {
            id = Model.id;
            nombre = Model.nombre;
            precio = Model.precio;
            descripcion = Model.descripcion;
            activo = Model.activo;
            user_uId = Model.user_uId;
            @default = Model.@default;
        }

        public int id { get; set; }
        public string nombre { get; set; }
        public Nullable<double> precio { get; set; }
        public string descripcion { get; set; }
        public Nullable<bool> activo { get; set; }
        public Nullable<System.Guid> user_uId { get; set; }
        public Nullable<bool> @default { get; set; }
    }

    public class ServicioModel : Service_Model
    {
        public Response Update()
        {
            Response res = new Response { Message = "Success", TypeOfResponse = TypeOfResponse.OK };
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
                Servicios ns = db.Servicios.Where(x => x.id == this.id).FirstOrDefault();
                ns.nombre = this.nombre;
                ns.precio = this.precio;
                ns.descripcion = this.descripcion;
                db.SaveChanges();
            }
             catch (Exception ex)
            {
                res.Data = null;
                res.TypeOfResponse = TypeOfResponse.ErrorResponse;
                res.Message = ex.Message;
                Common.Set_Log_Errors("Services Update -> Error: " + ex.ToString());
            }
            return res;
        }

        public Response Delete()
        {
            Response res = new Response { Message = "Success", TypeOfResponse = TypeOfResponse.OK };
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
                Servicios ns = db.Servicios.Where(x => x.id == this.id).FirstOrDefault();
                ns.activo = false;
                db.SaveChanges();
            }
             catch (Exception ex)
            {
                res.Data = null;
                res.TypeOfResponse = TypeOfResponse.ErrorResponse;
                res.Message = ex.Message;
                Common.Set_Log_Errors("Services Update -> Error: " + ex.ToString());

            }
            return res;
        }

        public Response Save(Guid user_uId)
        {
            Response res = new Response { Message = "Success", TypeOfResponse = TypeOfResponse.OK };
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
               
                Servicios ns = EntityFactory.getEntity<Servicios>(this, new Servicios());
                ns.activo = true;
                ns.@default = false;
                ns.user_uId = user_uId;
                db.Servicios.Add(ns);
                db.SaveChanges();
            }
             catch (Exception ex)
            {
                res.Data = null;
                res.TypeOfResponse = TypeOfResponse.ErrorResponse;
                res.Message = ex.Message;
                Common.Set_Log_Errors("Services Save -> Error: " + ex.ToString());

            }
            return res;
        }

    }

    public class CitaServicio
    {
        public Guid cita { get; set; }
        public int servicio { get; set; }
        public bool add { get; set; }

        public Response SetServicioToCita()
        {
            Response res = new Response { Message = "Success", TypeOfResponse = TypeOfResponse.OK };
            VL_CitasEntities db = new VL_CitasEntities();
            try
            {
                Citas cita = db.Citas.Where(x => x.uId == this.cita).FirstOrDefault();
                if (this.add)
                    cita.Servicios.Add(db.Servicios.Where(x => x.id == servicio).FirstOrDefault());
                else
                    cita.Servicios.Remove(db.Servicios.Where(x => x.id == servicio).FirstOrDefault());
                db.SaveChanges();
                try {
                    res.Data = cita.Servicios.Sum(x => x.precio);
                }
                catch { }
            }
             catch (Exception ex)
            {
                res.TypeOfResponse = TypeOfResponse.ErrorResponse;
                res.Message = ex.Message;
                res.Data = null;
                Common.Set_Log_Errors("SetServicioToCita -> Error: " + ex.ToString());

            }
            return res;
        }
    }
}