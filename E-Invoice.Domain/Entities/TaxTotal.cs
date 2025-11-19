namespace E_Invoice.Domain.Entities
{
    public class TaxTotal
    {
        public int Id { get; set; }
        public string TaxType { get; set; }
        public decimal Amount { get; set; }

        // RELATIONSHIP
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
