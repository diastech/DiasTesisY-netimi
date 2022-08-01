using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiasShared.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class EmailAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            string email = value as string;
            if (email == null)
            {
                return new ValidationResult("deneme");

            }
            var deneme = Regex.IsMatch(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

            if(deneme == false)
            {
                return new ValidationResult("email adresi");
            }            
                
        return ValidationResult.Success;
        }
    }
}
