//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KonKhayHoi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public admin()
        {
            this.Farms = new HashSet<Farm>();
            this.Partners = new HashSet<Partner>();
        }
    
        public string AID { get; set; }
        public string A_name { get; set; }
        public string A_lastname { get; set; }
        public string A_tel { get; set; }
        public string A_pass { get; set; }
        public Nullable<int> A_Investment { get; set; }
        public string A_no { get; set; }
        public string A_subD { get; set; }
        public string A_sub { get; set; }
        public string A_ProV { get; set; }
        public Nullable<int> Profit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Farm> Farms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partner> Partners { get; set; }
    }
}
