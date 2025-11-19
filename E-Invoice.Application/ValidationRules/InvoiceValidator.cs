using E_Invoice.Application.DTOs.PartyDtos;
using FluentValidation;

namespace E_Invoice.Application.Validators
{
    public class InvoiceValidator : AbstractValidator<InvoiceDto>
    {
        private readonly IActivityTypeProvider _taxCodeProvider;

        public InvoiceValidator(
            IActivityTypeProvider taxCodeProvider,
            IValidator<TaxTotalDto> taxTotalValidator,
            IValidator<InvoiceLineDto> invoiceLineValidator,
            IValidator<ReceiverDto> receiverValidator)
        {
            _taxCodeProvider = taxCodeProvider;

            RuleFor(invoice => invoice.DocumentType)
                .NotEmpty().WithMessage("DocumentType is required.")
                .Equal("i").WithMessage("DocumentType must be 'i'.");

            RuleFor(invoice => invoice.DocumentTypeVersion)
                .NotEmpty().WithMessage("DocumentTypeVersion is required.")
                .Equal("1.0").WithMessage("DocumentTypeVersion must be '1.0'.");

            RuleFor(invoice => invoice.DateTimeIssued)
                .NotEmpty().WithMessage("DateTimeIssued is required.")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("DateTimeIssued cannot be in the future.");

            RuleFor(invoice => invoice.TaxpayerActivityCode)
                .NotEmpty().WithMessage("TaxpayerActivityCode is required.")
                .Must(type => _taxCodeProvider.GetAll().Any(t => t.Code == type))
                .WithMessage("Invalid TaxpayerActivityCode — not found in allowed list.");

            RuleFor(invoice => invoice.InvoiceLines)
                .NotEmpty().WithMessage("Invoice must have at least one invoice line.");

            RuleFor(invoice => invoice.Signatures)
                .NotEmpty().WithMessage("At least one signature of the issuer must be present.");

            RuleForEach(invoice => invoice.TaxTotals)
                .SetValidator(taxTotalValidator);

            RuleForEach(invoice => invoice.InvoiceLines)
                .SetValidator(invoiceLineValidator);

            RuleFor(invoice => invoice.Receiver)
                .SetValidator(receiverValidator)
                .When(i => i.Receiver != null);

            RuleFor(inv => inv.TotalSalesAmount)
                .Must((inv, total) =>
                {
                    var expected = inv.InvoiceLines.Sum(l => l.SalesTotal);
                    return Math.Round(total, 5) == Math.Round(expected, 5);
                })
                .WithMessage("TotalSalesAmount must equal the sum of all InvoiceLine.SalesTotal.");

            RuleFor(inv => inv.TotalDiscountAmount)
                .Must((inv, discount) =>
                {
                    var expected = inv.InvoiceLines.Sum(l => l.Discount?.Amount ?? 0);
                    return Math.Round(discount, 5) == Math.Round(expected, 5);
                })
                .WithMessage("TotalDiscountAmount must equal the sum of all InvoiceLine Discount Amounts.");

            RuleFor(inv => inv.NetAmount)
                .Must((inv, netAmount) =>
                {
                    var expected = inv.InvoiceLines.Sum(l => l.NetTotal);
                    return Math.Round(netAmount, 5) == Math.Round(expected, 5);
                })
                .WithMessage("NetAmount must equal the sum of all InvoiceLine.NetTotal.");

            RuleFor(inv => inv.TotalItemsDiscountAmount)
                .Must((inv, value) =>
                {
                    var expected = inv.InvoiceLines.Sum(l => l.ItemsDiscount);
                    return Math.Round(value, 5) == Math.Round(expected, 5);
                })
                .WithMessage("TotalItemsDiscountAmount must equal the sum of all InvoiceLine.ItemsDiscount.");

            RuleFor(inv => inv.TotalAmount)
                .Must((inv, totalAmount) =>
                {
                    var linesTotal = inv.InvoiceLines.Sum(l => l.Total);
               
                    var discount = inv.ExtraDiscountAmount ?? 0m;
                
                    var calculated = linesTotal - discount;
                
                    return Math.Round(totalAmount, 2) == Math.Round(calculated, 2);
                })
                .WithMessage("TotalAmount is incorrect. It must equal Sum(InvoiceLines.Total) minus ExtraDiscountAmount.");
                
                    }
                }
}
