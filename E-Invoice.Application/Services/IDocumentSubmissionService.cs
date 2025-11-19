using E_Invoice.Application.DTOs.SubmissionDtos;
using E_Invoice.Application.DTOs;

namespace E_Invoice.Application.Services
{
    public interface IDocumentSubmissionService
    {
        Task<DocumentSubmissionResponseDto> SubmitDocumentsAsync(IEnumerable<InvoiceDto> validInvoices, IEnumerable<(InvoiceDto Invoice, Dictionary<string, string[]> Errors)> failedInvoices);
        Task CancelDocument(string Uuid);
    }
}
