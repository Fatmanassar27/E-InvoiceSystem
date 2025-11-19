namespace E_Invoice.Domain.Entities
{
    public class DocumentSubmission
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string SubmissionUUID { get; set; }
        [Required, MaxLength(50)]
        public string Format { get; set; }  = "json";
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public ICollection<AcceptedDocument> Documents { get; set; } = new List<AcceptedDocument>();
    }
}
