//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSED06_Aquatic_Pet_Store.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PET_INFO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PET_INFO()
        {
            this.PET_RECORD = new HashSet<PET_RECORD>();
        }
    
        public int ID_PK { get; set; }
        public int GROUP_FK { get; set; }
        public string COMMON { get; set; }
        public string SCIENTIFIC { get; set; }
    
        public virtual PET_GROUP PET_GROUP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PET_RECORD> PET_RECORD { get; set; }
    }
}
