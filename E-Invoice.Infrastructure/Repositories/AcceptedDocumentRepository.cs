using E_Invoice.Application.Interfaces;
using E_Invoice.Domain.Entities;
using E_Invoice.Infrastructure.Data;

namespace E_Invoice.Infrastructure.Repositories
{
    public class AcceptedDocumentRepository : GenericRepository<AcceptedDocument, int>, IAcceptedDocumentRepository
    {
        private readonly EInvoiceDbContext _dbcontext;

        public AcceptedDocumentRepository(EInvoiceDbContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<AcceptedDocument?> GetAcceptedDocumentByInvoiceIdAsync(int invoiceId)
        {
            return await _dbcontext.AcceptedDocuments
                .FirstOrDefaultAsync(a => a.InvoiceId == invoiceId);
        }
       
        public async Task<AcceptedDocument?> GetAcceptedDocumentByUuidAsync(string Uuid)
        {
            return await _dbcontext.AcceptedDocuments
                .FirstOrDefaultAsync(a => a.Uuid == Uuid);
        }

    }
}
