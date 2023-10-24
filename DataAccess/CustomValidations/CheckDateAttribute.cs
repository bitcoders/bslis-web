using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
namespace DataAccess.CustomValidations
{
    public class CheckDateAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = Convert.ToDateTime(value);
            if(date >= DateTime.Now.Date)
            {
                return ValidationResult.Success;
                //return base.IsValid(value, validationContext);
            }
            return new ValidationResult(this.ErrorMessage);
        }
    }
   
}
