//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VLCitas.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Citas_Attachments
    {
        public System.Guid uid { get; set; }
        public Nullable<System.Guid> cita_uId { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual Citas Citas { get; set; }
    }
}
