//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCVacationManagement.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLTURREZERVASYON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBLTURREZERVASYON()
        {
            this.TBLMUSTERI = new HashSet<TBLMUSTERI>();
        }
    
        public int id { get; set; }
        public Nullable<int> musteri { get; set; }
        public Nullable<int> tur { get; set; }
        public Nullable<decimal> ucret { get; set; }
        public Nullable<bool> durum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLMUSTERI> TBLMUSTERI { get; set; }
        public virtual TBLMUSTERI TBLMUSTERI1 { get; set; }
        public virtual TBLTUR TBLTUR { get; set; }
    }
}