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
    
    public partial class baiviet
    {
        public string mabv { get; set; }
        public string tenbv { get; set; }
        public string id { get; set; }
        public string anh { get; set; }
        public string noidung { get; set; }
    
        public virtual admin admin { get; set; }
    }
}
