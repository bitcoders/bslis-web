using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class MasterUserModel
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Compony Code")]
        public Nullable<byte> CompanyCode { get; set; }

        [Required]
        [Display(Name = "Active Unit Code")]
        public Nullable<int> UnitCode { get; set; }

        [Required]
        [Display(Name = "User Code")]
        [RegularExpression(pattern: @"^[0-9 a-z A-Z ]*$", ErrorMessage = "User Code can contains only number and alphabets!")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(pattern: @"^[a-z A-Z]*$", ErrorMessage = "Only alphabets are allowed!")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(pattern: @"^[a-z A-Z]*$", ErrorMessage = "Only alphabets are allowed!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [Display(Name = "Password")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "password must be {2} to {1} character long ")]
        [RegularExpression(pattern: @"^(?=(.*\d){2})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$", ErrorMessage = "Password must contain an Upper Case letter a number and a special character.")]

        public string Password { get; set; }

        // adding confirm password in this model only so that I can verify password and 
        // confirm password matches. There is no such field named confirmed password in database table
        [Required(ErrorMessage = "Confirm password can't be blank")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password & Confirm password must be same")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Created Date")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [Display(Name = "Created Date")]
        public string CreatedBy { get; set; }
        public string Salt { get; set; }


        [Display(Name = "Is Deleted")]

        public Nullable<bool> IsDeleted { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [Display(Name = "Mobile Number")]
        [RegularExpression(pattern: @"\d{10,10}$", ErrorMessage = "Accept only ten numbers ")]

        public string MobileNo { get; set; }

        [Display(Name = "Unit Rights")]
        public string UnitRights { get; set; }

        [Display(Name = "Email Veriried")]
        public Nullable<bool> EmailVerified { get; set; }
        [Display(Name = "Mobile Veriried")]
        public Nullable<bool> MobileVerified { get; set; }
        [Display(Name = "Newsletter")]
        public Nullable<bool> EmailNewLetter { get; set; }
        [Display(Name = "Mobile Alerts")]
        public Nullable<bool> MobileAlert { get; set; }

        [Display(Name = "Roles")]
        public string Role { get; set; }

        [Display(Name = "Dashboard Rights")]
        public string DashboardUnits { get; set; }

        [Display(Name = "Base Unit")]
        [RegularExpression(pattern: @"^[0-9]*$", ErrorMessage = "Working unit can contains only number!")]
        public Nullable<int> BaseUnit { get; set; }

        [Display(Name = "User Image URL")]
        public string UserImageUrl { get; set; }

        [Display(Name = "Entry Allowed Seasons")]
        public string EntryAllowedSeasons { get; set; }

        [Display(Name = "Modification Allowed Seasons")]
        public string ModificationAllowedForSeasons { get; set; }

        [Display(Name = "View Allowed Seasons")]
        public string ViewAllowedForSeasons { get; set; }

    }

    public class MasterUserUpdateModel
    {

        [Required]
        [Display(Name = "Compony Code")]
        public Nullable<byte> CompanyCode { get; set; }

        [Required]
        [Display(Name = "Unit Code")]
        public Nullable<int> UnitCode { get; set; }

        [Required]
        [Display(Name = "User Code")]
        [RegularExpression(pattern: @"^[0-9 a-z A-Z ]*$", ErrorMessage = "User Code can contains only number and alphabets!")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(pattern: @"^[a-z A-Z]*$", ErrorMessage = "Only alphabets are allowed!")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(pattern: @"^[a-z A-Z]*$", ErrorMessage = "Only alphabets are allowed!")]
        public string LastName { get; set; }


        [Display(Name = "Password")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "password must be {2} to {1} character long ")]
        [RegularExpression(pattern: @"^(?=(.*\d){2})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$", ErrorMessage = "Password must contain an Upper Case letter a number and a special character.")]

        public string Password { get; set; }

        // adding confirm password in this model only so that I can verify password and 
        // confirm password matches. There is no such field named confirmed password in database table

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password & Confirm password must be same")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Created Date")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [Display(Name = "Created Date")]
        public string CreatedBy { get; set; }
        public string Salt { get; set; }


        [Display(Name = "Is Deleted")]

        public Nullable<bool> IsDeleted { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Email Address length must be between {2} and {1}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [Display(Name = "Mobile Number")]
        [RegularExpression(pattern: @"\d{10,10}$", ErrorMessage = "Accept only ten numbers ")]

        public string MobileNo { get; set; }

        [Display(Name = "Unit Rights")]
        public string UnitRights { get; set; }

        [Display(Name = "Email Veriried")]
        public Nullable<bool> EmailVerified { get; set; }
        [Display(Name = "Mobile Veriried")]
        public Nullable<bool> MobileVerified { get; set; }
        [Display(Name = "Newsletter")]
        public Nullable<bool> EmailNewLetter { get; set; }
        [Display(Name = "Mobile Alerts")]
        public Nullable<bool> MobileAlert { get; set; }

        [Display(Name = "Roles")]
        public string Role { get; set; }

        [Display(Name = "Dashboard Rights")]
        public string DashboardUnits { get; set; }

        [Display(Name = "Base Unit")]
        [RegularExpression(pattern: @"^[0-9]*$", ErrorMessage = "Working unit can contains only number!")]
        public Nullable<int> BaseUnit { get; set; }

        [Display(Name = "Entry Allowed Seasons")]
        public string EntryAllowedSeasons { get; set; }

        [Display(Name = "Modification Allowed Seasons")]
        public string ModificationAllowedForSeasons { get; set; }

        [Display(Name = "View Allowed Seasons")]
        public string ViewAllowedForSeasons { get; set; }
    }

    public class MasterUserRolesModel
    {
        public string Code { get; set; }
        public Nullable<int> UnitCode { get; set; }

    }

    public class MasterUserPasswordModel
    {
        public string Code { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}