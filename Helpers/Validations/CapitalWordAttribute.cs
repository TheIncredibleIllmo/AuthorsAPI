using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAPI.Helpers.Validations
{
    public class CapitalWordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;

            var firstLetter = value.ToString()[0].ToString();

            return firstLetter == firstLetter.ToUpper() ? ValidationResult.Success : new ValidationResult("First letter should be Uppercase");
        }
    }
}
