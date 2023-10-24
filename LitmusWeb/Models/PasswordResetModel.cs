using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class PasswordResetModel
    {
        public int Id { get; set; }

        [Display(Name = "User Code")]
        public string UserCode { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime ValidFrom { get; set; }
        public System.DateTime ValidTill { get; set; }
        public bool IsActive { get; set; }
        public string RequestIP { get; set; }
    }
}