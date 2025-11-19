namespace E_Invoice.Application.DTOs.SubmissionDtos
{
    public class RejectedDocumentDto
    {
        public string InternalId { get; set; }
        public object Errors { get; set; }
    }
}
