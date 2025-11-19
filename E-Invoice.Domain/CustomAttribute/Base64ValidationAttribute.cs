using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace E_Invoice.Application.Validators.CustomAttribute
{
    public class Base64ValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var strValue = value as string;
            if (string.IsNullOrWhiteSpace(strValue))
                return ValidationResult.Success; 

            strValue = strValue.Trim();
            bool isValidBase64 = (strValue.Length % 4 == 0) &&
                                 Regex.IsMatch(strValue, @"^[a-zA-Z0-9\+/]*={0,3}$");

            if (isValidBase64)
                return ValidationResult.Success;

            return new ValidationResult("Signature value must be a valid Base64 encoded string.");
        }
    }
}
