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
    
    public partial class MasterVehicle
    {
        public Nullable<byte> CompanyCode { get; set; }
        public Nullable<int> UnitCode { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> MinimumWeight { get; set; }
        public Nullable<decimal> MaximumWeight { get; set; }
        public Nullable<decimal> AverageWeight { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int id { get; set; }
    
        public virtual MasterCompany MasterCompany { get; set; }
        public virtual MasterUnit MasterUnit { get; set; }
    }
}
