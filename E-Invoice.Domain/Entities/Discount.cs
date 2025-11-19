
namespace E_Invoice.Domain.Entities
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        [ForeignKey("InvoiceLine")]
        public int InvoiceLineId { get; set; }
        public virtual InvoiceLine InvoiceLine { get; set; }
    }
}
