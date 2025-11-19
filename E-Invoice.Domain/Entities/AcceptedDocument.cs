namespace E_Invoice.Domain.Entities
{
    public class AcceptedDocument
    {
        [Key] 
        public int Id { get; set; }
        [Required, MaxLength(26)]
        public string Uuid { get; set; } 
        [Required, MaxLength(42)]
        public string LongId { get; set; }
        public string InternalId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeVersion { get; set; }
        public string Status { get; set; }
        public string? ErrorMessage { get; set; }
        public int DocumentSubmissionId { get; set; }
        public DocumentSubmission DocumentSubmission { get; set; }
        public int InvoiceId { get; set; }
        [ForeignKey(nameof(InvoiceId))]
        public Invoice Invoice { get; set; }

    }
}
