
using System.ComponentModel.DataAnnotations;
namespace LitmusWeb.Models.CustomModels
{
    public class PasswordResetViewModel
    {
        [Display(Name = "User Code")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid E-mail address!")]
        [Required(ErrorMessage = "Email address required!")]
        public string UserCode { get; set; }
    }

    public class PasswordChangeViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S{6,20}$",
         ErrorMessage = "Minimum 6 characters, 1 Alphabet, 1 Number and 1 Special Character required!")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, type again!")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}