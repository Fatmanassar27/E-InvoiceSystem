namespace E_Invoice.Domain.Entities
{
    public class InvoiceLine
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "ItemType is required")]
        [RegularExpression("^(GS1|EGS)$", ErrorMessage = "ItemType must be either 'GS1' or 'EGS'")]
        public string ItemType { get; set; }
        [Required(ErrorMessage = "ItemCode is required")]
        public string ItemCode { get; set; }
        [Required(ErrorMessage = "UnitType is required")]
        public string UnitType { get; set; }
        [Range(0.00001, double.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public decimal Quantity { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "SalesTotal must be non-negative.")]
        public decimal SalesTotal { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Total must be non-negative.")]
        public decimal Total { get; set; }
        public decimal? ValueDifference { get; set; } = 0;
        public decimal? TotalTaxableFees { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "NetTotal must be non-negative.")]
        public decimal NetTotal { get; set; }
        public decimal? ItemsDiscount { get; set; }
        public string? InternalCode { get; set; }

        // RELATIONSHIP
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Discount? Discount { get; set; }
        public virtual ICollection<TaxableItem> TaxableItems { get; set; } = new HashSet<TaxableItem>();
        public virtual Value UnitValue { get; set; }
    }
}
