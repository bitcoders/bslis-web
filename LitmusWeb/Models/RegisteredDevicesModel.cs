using DataAccess;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class RegisteredDevicesModel
    {
        [Display(Name = "Code")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Code can not be empty.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Code length must be between {2} to {1}.")]
        public string Code { get; set; }


        [Display(Name = "Initials")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Initials can not be empty.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Initials length must be between {1} to {2}.")]
        public string Gender { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name can not be empty.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name length must be between {1} to {2}.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name can not be empty.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name length must be between {1} to {2}.")]
        public string LastName { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "User Image Url")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Image Url can not be empty.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "User Image Url length must be between {1} to {2}.")]
        public string ImageUrl { get; set; }

        [Display(Name = "User Code")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Code can not be empty.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "User Code length must be between {2} to {1}.")]
        public string UserCode { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "User Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Code can not be empty.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "User Code length must be between {2} to {1}.")]
        public string UserPassword { get; set; }

        [Display(Name = "Salt")]
        public string Salt { get; set; }

        [Required]
        [Display(Name = "Device Type")]
        public int DeviceTypeCode { get; set; }

        [Display(Name = "Device Token")]
        public string DeviceToken { get; set; }

        [Display(Name = "Access Token")]
        public string AccessToken { get; set; }

        [Display(Name = "Unit Rights")]
        public string UnitRights { get; set; }

        [Display(Name = "Menu Rights")]
        public string MenuRights { get; set; }

        [Display(Name = "Is Active")]
        [Required]
        public bool IsActive { get; set; }

        [Display(Name = "Valid From")]
        [DataType(DataType.Date)]
        [Required]
        public System.DateTime ValidFrom { get; set; }

        [Display(Name = "Valid Till")]
        [DataType(DataType.Date)]
        [Required]
        public System.DateTime ValidTill { get; set; }

        [Display(Name = "Created At")]
        [DataType(DataType.Date)]

        public System.DateTime CreatedAt { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Department Code")]
        [Required]
        public int DepartmentCode { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        public string DeviceType { get; set; }

        public string DepartmentName { get; set; }
        public virtual MasterDeviceTypeModel MasterDeviceTypeModel { get; set; }

        public virtual MasterDepartment MasterDepartment { get; set; }

        public virtual ICollection<RegisteredDevice> RegisteredDevices { get; set; }
    }


    public class RegisteredDevicesUpdateModel
    {
        [Display(Name = "Code")]
        public string Code { get; set; }


        [StringLength(50, MinimumLength = 1, ErrorMessage = "User Code length must be between {2} to {1}.")]
        public string UserCode { get; set; }

        [Display(Name = "Initials")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Initials can not be empty.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Initials length must be between {1} to {2}.")]
        public string Gender { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name can not be empty.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name length must be between {1} to {2}.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name can not be empty.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name length must be between {1} to {2}.")]
        public string LastName { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "User Image Url")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Image Url can not be empty.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "User Image Url length must be between {1} to {2}.")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Device Type")]
        public int DeviceTypeCode { get; set; }


        [Display(Name = "Access Token")]
        public string AccessToken { get; set; }

        [Display(Name = "Unit Rights")]
        public string UnitRights { get; set; }

        [Display(Name = "Menu Rights")]
        public string MenuRights { get; set; }

        [Display(Name = "Is Active")]
        [Required]
        public bool IsActive { get; set; }

        [Display(Name = "Valid From")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}:dd-MM-yyyy")]
        [Required]
        public System.DateTime ValidFrom { get; set; }

        [Display(Name = "Valid Till")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}:dd-MM-yyyy")]
        [Required]
        public System.DateTime ValidTill { get; set; }


        [Display(Name = "Department Code")]
        [Required]
        public int DepartmentCode { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        public string DeviceType { get; set; }

        public string DepartmentName { get; set; }
        public virtual MasterDeviceTypeModel MasterDeviceTypeModel { get; set; }

        public virtual MasterDepartment MasterDepartment { get; set; }

        public virtual ICollection<RegisteredDevice> RegisteredDevices { get; set; }
    }

}