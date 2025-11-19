using E_Invoice.Application.Validators.CustomAttribute;

namespace E_Invoice.Domain.Entities
{
    public class Signature
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Signature type is required.")]
        [SignatureTypeValidation]
        public string Type { get; set; }
        [Required(ErrorMessage = "Signature value is required.")]
        [Base64Validation]
        public string Value { get; set; }

        // RELATIONSHIP
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
