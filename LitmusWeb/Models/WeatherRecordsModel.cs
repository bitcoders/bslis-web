using System;
using System.ComponentModel.DataAnnotations;
namespace LitmusWeb.Models
{
    public class WeatherRecordsModel
    {
        public int Id { get; set; }
        public int UnitCode { get; set; }

        [Required]
        [Display(Name = "Season Year")]
        public int SeasonCode { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Record Date")]
        public System.DateTime RecordDate { get; set; }

        [Required]
        [Display(Name = "Minimum Temp.")]
        public Nullable<decimal> TemperatureMin { get; set; }

        [Required]
        [Display(Name = "Maximum Temp.")]
        public Nullable<decimal> TemperatureMax { get; set; }

        [Display(Name = "Humidity")]
        public Nullable<decimal> Humidity { get; set; }

        [Display(Name = "Wind Speed (Km/h)")]
        public Nullable<decimal> WindSpeed { get; set; }

        [Display(Name = "Rainfall (mm)")]
        public Nullable<decimal> RainFallMm { get; set; }

        [Display(Name = "UV Index")]
        public Nullable<decimal> UvIndex { get; set; }

        [Display(Name = "Weather Condition")]
        [Required]
        [Range(maximum: 15, minimum: 1, ErrorMessage = "Please select an appropriate weather condition!")]
        public string WeatherType { get; set; }

        [Display(Name = "Weather conditions (all day)")]
        public string AllWeatherConditions { get; set; }

        public virtual WeatherTypeModel WeatherType1
        {
            get; set;
        }
    }
}