

namespace E_Invoice.Application.Validators
{
    public class TaxableItemValidator : AbstractValidator<TaxableItemDto>
    {
        private readonly ITaxableTypeProvider _taxableTypeProvider;
        private readonly ITaxableSubtypeProvider _taxableSubtypeProvider;
        public TaxableItemValidator(ITaxableTypeProvider taxableITypeProvider , ITaxableSubtypeProvider taxableSubtypeProvider)
        {
            _taxableTypeProvider = taxableITypeProvider;
            _taxableSubtypeProvider = taxableSubtypeProvider;

            RuleFor(t => t.TaxType)
                .NotEmpty()
                .WithMessage("TaxType is required.")
                .Must(type => _taxableTypeProvider.GetAll().Any(t => t.Code == type))
                .When(t => !string.IsNullOrEmpty(t.TaxType))
                .WithMessage("Invalid Tax Type — not found in allowed list.");

            RuleFor(t => t.Amount)
               .GreaterThanOrEqualTo(0)
               .WithMessage("Amount must be non-negative.")
               .HasMaxDecimalPlaces(5)
               .WithMessage("Amount allows up to 5 decimal digits.");

            RuleFor(t => t.Rate)
               .HasMaxDecimalPlaces(5)
               .WithMessage("Rate allows up to 5 decimal digits.");

            RuleFor(t => t.Rate)
               .Equal(0)
               .WithMessage("Rate must be 0 when Tax Type is T3 (Fixed Amount) or T6 (Stamping Tax).")
               .When(t => t.TaxType == "T3" || t.TaxType == "T6");

            RuleFor(t => t.SubType)
               .Must(type => _taxableSubtypeProvider.GetAll().Any(t => t.Code == type))
                .When(t => !string.IsNullOrEmpty(t.SubType))
               .WithMessage("Invalid Sub Type — not found in allowed list.");

        }
    }
}
