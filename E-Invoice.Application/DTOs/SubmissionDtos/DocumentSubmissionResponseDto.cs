namespace E_Invoice.Application.DTOs.SubmissionDtos
{
    public class DocumentSubmissionResponseDto
    {
        public string SubmissionUUID { get; set; }
        public List<AcceptedDocumentDto> AcceptedDocuments { get; set; } = new();
        public List<RejectedDocumentDto> RejectedDocuments { get; set; } = new();
    }
}
