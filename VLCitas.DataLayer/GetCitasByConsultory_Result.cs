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
    
    public partial class GetCitasByConsultory_Result
    {
        public System.Guid id { get; set; }
        public string title { get; set; }
        public Nullable<System.DateTime> start { get; set; }
        public Nullable<System.DateTime> end { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<int> status_id { get; set; }
        public string color { get; set; }
        public Nullable<System.Guid> doctor_uid { get; set; }
        public string doctor_name { get; set; }
    }
}
