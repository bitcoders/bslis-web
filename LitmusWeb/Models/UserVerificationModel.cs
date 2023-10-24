using System;
using System.ComponentModel.DataAnnotations;
namespace LitmusWeb.Models
{
    public class UserVerificationModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Unit Code")]
        public Nullable<int> UnitCode { get; set; }

        [Display(Name = "User Code")]
        public string UserCode { get; set; }

        [Display(Name = "Activation Link")]
        public string ActivationLink { get; set; }

        [Required]
        [Display(Name = "Activation Code")]
        public string ActivationCode { get; set; }

        [Required]
        [Display(Name = "Valid till")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime ActivationValidity { get; set; }

        [Display(Name = "Activated At")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> ActivatedAt { get; set; }

        [Required]
        [Display(Name = "Created At")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime CreatedDate { get; set; }
    }
}