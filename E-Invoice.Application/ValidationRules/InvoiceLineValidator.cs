namespace E_Invoice.Application.Validators
{
    public class InvoiceLineValidator : AbstractValidator<InvoiceLineDto>
    {
        private readonly IUnitTypeProvider _unitTypeProvider;
        public InvoiceLineValidator(IValidator<DiscountDto> discount, IValidator<TaxableItemDto> taxableitem, IUnitTypeProvider unitTypeProvider)
        {
            _unitTypeProvider = unitTypeProvider;
            RuleFor(l => l.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.")
                .HasMaxDecimalPlaces(5).WithMessage("Quantity allows up to 5 decimal digits.");

            RuleFor(l => l.SalesTotal).GreaterThan(0).WithMessage("SalesTotal must be non-negative.")
                .HasMaxDecimalPlaces(5).WithMessage("SalesTotal allows up to 5 decimal digits.");

            RuleFor(l => l.Total).GreaterThan(0).WithMessage("Total must be non-negative.")
                .HasMaxDecimalPlaces(5).WithMessage("Total allows up to 5 decimal digits.");

            RuleFor(l => l.NetTotal).GreaterThan(0).WithMessage("NetTotal must be non-negative.")
                .HasMaxDecimalPlaces(5).WithMessage("NetTotal allows up to 5 decimal digits.");

            RuleFor(l => l.UnitType).NotEmpty().WithMessage("UnitType is required.")
                .Must(type => _unitTypeProvider.GetAll().Any(t => t.Code == type))
                .When(t => !string.IsNullOrEmpty(t.UnitType)).WithMessage("Invalid Unit Type — not found in allowed list.");

            RuleFor(l => l.Discount)
                .SetValidator(discount)
                .When(l => l.Discount != null);

            RuleForEach(l => l.TaxableItems)
                .SetValidator(taxableitem)
                .When(l => l.TaxableItems != null && l.TaxableItems.Any());

            RuleFor(l => l.TaxableItems)
                .Must(items => items == null || items
                .Select(i => i.TaxType).Distinct().Count() == items.Count)
                .WithMessage("Each TaxType in TaxableItems must be unique within the same invoice line.");

            RuleFor(l => l.SalesTotal)
                .Must((line, salesTotal) => salesTotal == Math.Round(line.UnitValue.AmountEGP * line.Quantity, 2))
                .WithMessage("SalesTotal must be equal to unit value Amount multiplied by Quantity");

            When(x => x.Discount != null && x.Discount.Rate.HasValue && x.Discount.Rate.Value > 0, () =>
            {
                RuleFor(x => x.Discount.Amount)
                .Must((line, discountAmount) => discountAmount == Math.Round(line.Discount.Rate.Value * line.SalesTotal, 2))
                .WithMessage("Discount.Amount must equal Discount.Rate * SalesTotal.");
            });

            RuleFor(l => l.NetTotal).Must((line, netTotal) => netTotal == Math.Round(line.SalesTotal - line.Discount?.Amount ?? 0, 2))
                .WithMessage("NetTotal must equal SalesTotal - Discount.Amount.");


            RuleForEach(line => line.TaxableItems)
                .Where(t => t.TaxType != "T2" && t.TaxType != "T3" && t.TaxType != "T6" && t.TaxType != "T1" && t.TaxType != "T4")
                .Must((line, taxableItem) =>
                {
                    if (taxableItem.Rate > 0)
                        return taxableItem.Amount == Math.Round(taxableItem.Rate * line.NetTotal, 2);
                    return true;
                }).WithMessage("TaxableItem.Amount must equal Rate * NetTotal.");

            RuleFor(x => x.TotalTaxableFees)
                .Must((line, totalFees) => totalFees == line.TaxableItems
                .Where(t => t.TaxType != "T2" && t.TaxType != "T3" && t.TaxType != "T6" && t.TaxType != "T1" && t.TaxType != "T4")
                .Sum(x => x.Amount))
                .WithMessage("TotalTaxableFees must equal the sum of all TaxableItem.Amounts.");

            RuleForEach(line => line.TaxableItems)
                .Where(t => t.TaxType is "T3" or "T6").Must(tax => tax.Rate == 0)
                .WithMessage("For TaxTypes T3, T6 , Rate must always be 0 because it is a fixed amount tax.");

            RuleForEach(line => line.TaxableItems)
                .Where(t => t.TaxType == "T2")
                .Must((line, tax) =>
                {
                    var t3Amount = line.TaxableItems.Where(x => x.TaxType == "T3").Sum(x => x.Amount);
                    var baseAmount = line.NetTotal + line.TotalTaxableFees + line.ValueDifference + t3Amount;
                    return tax.Amount == baseAmount * tax.Rate;
                })
                .WithMessage("For T2 (Table Tax Percentage), Amount must equal (NetTotal + TotalTaxableFees + ValueDifference + T3.Amount) * Rate.");

            RuleForEach(line => line.TaxableItems)
                .Where(t => t.TaxType == "T1")
                .Must((line, tax) =>
                {
                    var taxableFeesBase = line.TaxableItems
                        .Where(x => x.TaxType != "T1" && x.TaxType != "T4")
                        .Sum(x => x.Amount);
               
                    var baseAmount = line.NetTotal + line.ValueDifference + taxableFeesBase;
               
                    var expectedVat = baseAmount * tax.Rate;
               
                    return Math.Abs(Math.Round(tax.Amount, 2) - Math.Round(expectedVat,2 )) < 0.01m;
                })
                .WithMessage("VAT (T1) Amount must equal (NetTotal + ValueDifference + Sum(All taxable fees except T1 and T4)) * Rate.");

            RuleForEach(line => line.TaxableItems)
                .Where(t => t.TaxType == "T4")
                .Must((line, tax) =>
                {
                    var baseAmount = line.NetTotal - line.ItemsDiscount;
                    return Math.Round(tax.Amount, 5) == Math.Round(baseAmount * tax.Rate, 5);
                })
                .WithMessage("Withholding Tax (T4) Amount must equal Rate × (NetTotal – ItemsDiscount).");
            RuleFor(line => line.Total)
                .Must((line, total) =>
                {
                    var taxableExceptWHT = line.TaxableItems
                        .Where(t => t.TaxType != "T4")
                        .Sum(t => t.Amount);
                
                    var whtTaxes = line.TaxableItems
                        .Where(t => t.TaxType == "T4")
                        .Sum(t => t.Amount);
                    var calculated = line.NetTotal
                                      + line.TotalTaxableFees   
                                      + line.ValueDifference    
                                      + taxableExceptWHT
                                      - line.ItemsDiscount
                                      - whtTaxes;
                
                    return Math.Round(total, 2) == Math.Round(calculated, 2);
                })
                .WithMessage("Line Total is incorrect. It must equal: NetTotal + TotalTaxableFees + ValueDifference + Sum(TaxableItems except T4) - ItemsDiscount - Sum(T4).");
        }
    }
}