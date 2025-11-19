namespace E_Invoice.Application.Validators.CustomAttribute
{
    public class SignatureTypeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var type = value as string;
            if (string.IsNullOrWhiteSpace(type))
                return ValidationResult.Success; 

            if (type == "I" || type == "S")
                return ValidationResult.Success;

            return new ValidationResult("Signature type must be either 'I' (Issuer) or 'S' (ServiceProvider).");
        }
    }
}
