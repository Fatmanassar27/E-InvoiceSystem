using System.ComponentModel.DataAnnotations;

namespace E_Invoice.Domain.Entities
{
    public class Invoice
    {
        #region Basic Info
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^i$", ErrorMessage = "DocumentType must be 'i'.")]
        public string DocumentType { get; set; }

        [Required]
        [RegularExpression(@"^1\.0$", ErrorMessage = "DocumentTypeVersion must be '1.0'.")]
        public string DocumentTypeVersion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeIssued { get; set; }

        public string TaxpayerActivityCode { get; set; }
        #endregion

        #region References
        public string? PurchaseOrderReference { get; set; }
        public string? PurchaseOrderDescription { get; set; }
        public string? SalesOrderReference { get; set; }
        public string? SalesOrderDescription { get; set; }

        [MaxLength(50)]
        public string? ProformaInvoiceNumber { get; set; }
        #endregion

        #region Relationships
        public virtual Issuer Issuer { get; set; }
        public virtual Receiver Receiver { get; set; }

        public string InternalId { get; set; }

        public virtual Payment? Payment { get; set; }
        public virtual Delivery? Delivery { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Invoice must have at least one invoice line.")]
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

        public virtual ICollection<TaxTotal> TaxTotals { get; set; } = new List<TaxTotal>();

        [MinLength(1, ErrorMessage = "At least one signature of the issuer must be present.")]
        public virtual ICollection<Signature> Signatures { get; set; } = new List<Signature>();
        #endregion

        #region Totals
        public decimal TotalSalesAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal ExtraDiscountAmount { get; set; }
        public decimal TotalItemsDiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        #endregion

        #region Dates
        public DateTime? ServiceDeliveryDate { get; set; }
        #endregion

        #region Submission
       
        //public virtual InvoiceSubmission? Submission { get; set; }
        #endregion
    }
}
