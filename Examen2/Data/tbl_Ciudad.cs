//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Examen2.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Ciudad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Ciudad()
        {
            this.tbl_CiudadOrigenDestino = new HashSet<tbl_CiudadOrigenDestino>();
            this.tbl_CiudadOrigenDestino1 = new HashSet<tbl_CiudadOrigenDestino>();
            this.tbl_Vuelo = new HashSet<tbl_Vuelo>();
            this.tbl_Vuelo1 = new HashSet<tbl_Vuelo>();
        }
    
        public int id_Ciudad { get; set; }
        public string Ciudad { get; set; }
        public string Descripcion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CiudadOrigenDestino> tbl_CiudadOrigenDestino { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CiudadOrigenDestino> tbl_CiudadOrigenDestino1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Vuelo> tbl_Vuelo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Vuelo> tbl_Vuelo1 { get; set; }
    }
}
