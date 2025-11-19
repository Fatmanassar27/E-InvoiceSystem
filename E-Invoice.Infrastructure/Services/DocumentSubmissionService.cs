using System.Reflection.Metadata;
using AutoMapper;
using E_Invoice.Application.DTOs;
using E_Invoice.Application.DTOs.SubmissionDtos;
using E_Invoice.Application.Interfaces;
using E_Invoice.Application.Services;
using E_Invoice.Domain.Entities;
using E_Invoice.Domain.Enums;

namespace E_Invoice.Infrastructure.Services
{
    public class DocumentSubmissionService : IDocumentSubmissionService
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentSubmissionService(IInvoiceService invoiceService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _invoiceService = invoiceService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DocumentSubmissionResponseDto> SubmitDocumentsAsync(IEnumerable<InvoiceDto> validInvoices, IEnumerable<(InvoiceDto Invoice, Dictionary<string, string[]> Errors)> failedInvoices)
        {
            var submission = new DocumentSubmission
            {
                SubmissionUUID = Guid.NewGuid().ToString(),
                SubmittedAt = DateTime.UtcNow,
                Documents = new List<AcceptedDocument>()
            };

            var response = new DocumentSubmissionResponseDto
            {
                SubmissionUUID = submission.SubmissionUUID,
                AcceptedDocuments = new List<AcceptedDocumentDto>(),
                RejectedDocuments = new List<RejectedDocumentDto>()
            };

            foreach (var invoiceDto in validInvoices)
            {
                var invoice = _mapper.Map<Invoice>(invoiceDto);
                await _invoiceService.AddInvoiceAsync(invoice);
                await _unitOfWork.SaveChangesAsync();

                var accepted = new AcceptedDocument
                {
                    InvoiceId = invoice.Id,
                    DocumentSubmission = submission,
                    InternalId = invoice.InternalId,
                    Uuid = Guid.NewGuid().ToString().Substring(0, 26),
                    LongId = Guid.NewGuid().ToString(),
                    Status = StatusEnums.Accepted.ToString(),
                };

                submission.Documents.Add(accepted);
                response.AcceptedDocuments.Add(_mapper.Map<AcceptedDocumentDto>(accepted));
            }

            foreach (var failed in failedInvoices)
            {
                response.RejectedDocuments.Add(new RejectedDocumentDto
                {
                    InternalId = failed.Invoice.InternalId,
                    Errors = failed.Errors,
                    Status = StatusEnums.Rejected.ToString()
                });
            }

            if (submission.Documents.Count > 0)
            {
                await _unitOfWork.GetRepository<DocumentSubmission, int>().AddAsync(submission);
                await _unitOfWork.SaveChangesAsync();
            }

            return response;
        }
        public async Task CancelDocument(string Uuid)
        {
            var repo = _unitOfWork.AcceptedDocumentRepository;

            var existingDoc = await repo.GetAcceptedDocumentByUuidAsync(Uuid);
            if(existingDoc == null)
               throw new KeyNotFoundException("Document not found");

            existingDoc.Status = StatusEnums.Cancelled.ToString();
            repo.Update(existingDoc);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}


