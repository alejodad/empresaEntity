//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Empresa
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_depto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_depto()
        {
            this.tbl_ciudad = new HashSet<tbl_ciudad>();
        }
    
        public int idDepto { get; set; }
        public string nombreDepto { get; set; }
        public Nullable<int> idPais { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ciudad> tbl_ciudad { get; set; }
        public virtual tbl_pais tbl_pais { get; set; }
    }
}
