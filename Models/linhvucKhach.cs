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
    
    public partial class linhvucKhach
    {
        public string idkhach { get; set; }
        public string malv { get; set; }
        public Nullable<int> Solan { get; set; }
    
        public virtual khach khach { get; set; }
        public virtual linhvuc linhvuc { get; set; }
    }
}