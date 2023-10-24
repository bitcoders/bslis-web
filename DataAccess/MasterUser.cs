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
    
    public partial class MasterUser
    {
        public int Id { get; set; }
        public Nullable<byte> CompanyCode { get; set; }
        public Nullable<int> UnitCode { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public Nullable<bool> EmailVerified { get; set; }
        public Nullable<bool> MobileVerified { get; set; }
        public Nullable<bool> EmailNewLetter { get; set; }
        public Nullable<bool> MobileAlert { get; set; }
        public string UnitRights { get; set; }
        public string Role { get; set; }
        public string DashboardUnits { get; set; }
        public Nullable<int> BaseUnit { get; set; }
        public string UserImageUrl { get; set; }
        public string EntryAllowedSeasons { get; set; }
        public string ModificationAllowedForSeasons { get; set; }
        public string ViewAllowedForSeasons { get; set; }
    
        public virtual MasterCompany MasterCompany { get; set; }
        public virtual MasterUnit MasterUnit { get; set; }
    }
}
