
namespace E_Invoice.Domain.Entities
{
    public class TaxableItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "TaxType is required.")]
        public string TaxType { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be non-negative.")]
        public decimal Amount { get; set; }
        public string? SubType { get; set; }
        [Range(0, 999, ErrorMessage = "Rate must be between 0 and 999.")]
        public decimal Rate { get; set; }
        // RELATIONSHIP
        [ForeignKey(nameof(TaxableItem))]
        public int InvoiceLineId { get; set; }
        public virtual InvoiceLine InvoiceLine { get; set; } = null!;
    }
}
