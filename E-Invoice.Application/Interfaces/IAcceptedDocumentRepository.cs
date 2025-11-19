namespace E_Invoice.Application.Interfaces
{
    public interface IAcceptedDocumentRepository : IGenericRepository<AcceptedDocument, int>
    {
        Task<AcceptedDocument> GetAcceptedDocumentByInvoiceIdAsync(int invoiceId);
        Task<AcceptedDocument> GetAcceptedDocumentByUuidAsync(string Uuid);
    }
}