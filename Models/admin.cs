//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLiKOL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public admin()
        {
            this.baiviets = new HashSet<baiviet>();
            this.hopdongs = new HashSet<hopdong>();
        }
    
        public string id { get; set; }
        public string nickname { get; set; }
        public string ten { get; set; }
        public Nullable<System.DateTime> ngaysinh { get; set; }
        public string gioitinh { get; set; }
        public string diachi { get; set; }
        public string sdt { get; set; }
        public string gmail { get; set; }
        public string pass { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<baiviet> baiviets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hopdong> hopdongs { get; set; }
    }
}
