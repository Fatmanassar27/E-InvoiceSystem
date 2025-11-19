namespace E_Invoice.Domain.Entities
{
    public class Issuer : Party
    {
        [Required(ErrorMessage = "Address is required.")]
        public virtual IssuerAddress Address { get; set; }
    }
}
