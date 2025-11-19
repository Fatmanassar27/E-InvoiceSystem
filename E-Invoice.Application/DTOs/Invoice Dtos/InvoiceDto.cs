using E_Invoice.Application.DTOs.PartyDtos;

namespace E_Invoice.Application.DTOs
{
    public class InvoiceDto 
    {
        public string DocumentType { get; set; } 
        public string DocumentTypeVersion { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public string TaxpayerActivityCode { get; set; }
        public string InternalId { get; set; }
        public IssuerDto Issuer { get; set; }
        public ReceiverDto Receiver { get; set; }
        public List<InvoiceLineDto> InvoiceLines { get; set; }
        public List<TaxTotalDto> TaxTotals { get; set; }
        public List<SignatureDto> Signatures { get; set; }

        public decimal TotalSalesAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal? ExtraDiscountAmount { get; set; }
        public decimal TotalItemsDiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
