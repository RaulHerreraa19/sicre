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
    
    public partial class TimeZones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeZones()
        {
            this.Offices_Configuration = new HashSet<Offices_Configuration>();
        }
    
        public int id { get; set; }
        public string TimeZoneId { get; set; }
        public string DaylightName { get; set; }
        public string DisplayName { get; set; }
        public string StandardName { get; set; }
        public string BaseUtcOffset { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Offices_Configuration> Offices_Configuration { get; set; }
    }
}
