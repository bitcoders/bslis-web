using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class WeatherTypeModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Weather Type Code is required!")]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Weather Type Short Name is required!")]
        [Display(Name = "Short Name")]
        public string Text { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}