//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRight
    {
        public int Id { get; set; }
        public int CompCode { get; set; }
        public int UnitCode { get; set; }
        public string UserCode { get; set; }
        public Nullable<int> MasterMenuCode { get; set; }
        public Nullable<int> MasterSubMenuCode { get; set; }
        public bool IsActive { get; set; }
        public bool Creates { get; set; }
        public bool Reads { get; set; }
        public bool Updates { get; set; }
        public bool Deletes { get; set; }
        public string RightsString { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual MasterUnit MasterUnit { get; set; }
    }
}
