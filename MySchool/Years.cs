//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MySchool
{
    using System;
    using System.Collections.Generic;
    
    public partial class Years
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Years()
        {
            this.Stages = new HashSet<Stages>();
            this.MONTHS = new HashSet<MONTH>();
        }
    
        public int YearID { get; set; }
        public System.DateTime YearDateStart { get; set; }
        public System.DateTime YearDateEnd { get; set; }
        public System.DateTime HireDate { get; set; }
        public bool Active { get; set; }
        public int SchoolID { get; set; }
    
        public virtual Schools Schools { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stages> Stages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MONTH> MONTHS { get; set; }
    }
}
