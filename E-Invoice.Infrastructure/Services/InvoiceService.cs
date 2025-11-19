using AutoMapper;
using E_Invoice.Application.DTOs;
using E_Invoice.Application.Interfaces;
using E_Invoice.Application.Services;
using E_Invoice.Domain.Entities;
using E_Invoice.Domain.Enums;

namespace E_Invoice.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            var repo = _unitOfWork.GetRepository<Invoice, int>();
            var invoices = await repo.GetAllAsync();
            return invoices;
        }
        public async Task<Invoice?> GetInvoiceByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Invoice, int>();
            var invoice = await repo.GetByIdAsync(id);
            return invoice;
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            var repo = _unitOfWork.GetRepository<Invoice, int>();
            await repo.AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateInvoiceAsync(int id, InvoiceDto updatedInvoice)
        {
            var repo = _unitOfWork.GetRepository<Invoice, int>();

            var existingInvoice = await repo.GetByIdAsync(id);
            if (existingInvoice == null)
                throw new Exception("Invoice not found");

            var acceptedRepo = _unitOfWork.AcceptedDocumentRepository;
            var acceptedDoc = await acceptedRepo.GetAcceptedDocumentByInvoiceIdAsync(id);

            if (acceptedDoc != null)
            {
                acceptedRepo.Update(acceptedDoc);
            }

            _mapper.Map(updatedInvoice, existingInvoice);

            repo.Update(existingInvoice);
            await _unitOfWork.SaveChangesAsync();
        }

       
    }
}
