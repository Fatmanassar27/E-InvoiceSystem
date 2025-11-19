using E_Invoice.Application.DTOs;
using E_Invoice.Application.Interfaces.IProviders;
using E_Invoice.Application.Validators.Extention;

namespace E_Invoice.Application.Validators
{
    public class TaxTotalValidator : AbstractValidator<TaxTotalDto>
    {
        private readonly ITaxableTypeProvider _taxableTypeProvider;
        public TaxTotalValidator(ITaxableTypeProvider taxableTypeProvider)
        {
            _taxableTypeProvider = taxableTypeProvider;

            RuleFor(t => t.TaxType)
                .NotEmpty()
                .WithMessage("TaxType is required.")
                .Must(taxType => _taxableTypeProvider.GetAll().Any(t => t.Code == taxType))
                .WithMessage("TaxType is invalid.");
           
            RuleFor(t => t.Amount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Amount must be non-negative.")
                .HasMaxDecimalPlaces(5)
                .WithMessage("Amount allows up to 5 decimal digits.");
        }
    }
}
