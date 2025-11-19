using E_Invoice.Application.Interfaces.IProviders;
using E_Invoice.Application.Validators.Extention;
using FluentValidation;

namespace E_Invoice.Application.Validators
{
    public class DeliveryValidator : AbstractValidator<DeliveryDto>
    {
        private readonly ICountryCodeProvider _countryCodeProvider;

        public DeliveryValidator(ICountryCodeProvider countryCodeProvider)
        {
            _countryCodeProvider = countryCodeProvider;

            RuleFor(d => d.DateValidity)
                .GreaterThan(DateTime.Now)
                .When(d => d.DateValidity != null)
                .WithMessage("DateValidity cannot be in the past.");

            RuleFor(d => d.CountryOfOrigin)
                .Length(2)
                .When(d => !string.IsNullOrEmpty(d.CountryOfOrigin))
                .WithMessage("CountryOfOrigin must be a 2-letter ISO-3166-2 code.")
                .Must(code => _countryCodeProvider.GetAll().Any(t => t.Code == code))
                .When(d => !string.IsNullOrEmpty(d.CountryOfOrigin))
                .WithMessage("Invalid CountryOfOrigin code — not found in allowed list.");

            RuleFor(d => d.GrossWeight)
                .GreaterThan(0)
                .When(d => d.GrossWeight.HasValue)
                .WithMessage("GrossWeight must be positive.")
                .HasMaxDecimalPlaces(5)
                .When(d => d.GrossWeight.HasValue)
                .WithMessage("GrossWeight allows up to 5 decimal digits.");

            RuleFor(d => d.NetWeight)
                .GreaterThan(0)
                .When(d => d.NetWeight.HasValue)
                .WithMessage("NetWeight must be positive.")
                .HasMaxDecimalPlaces(5)
                .When(d => d.NetWeight.HasValue)
                .WithMessage("NetWeight allows up to 5 decimal digits.");

            RuleFor(d => d)
                .Must(d =>
                {
                    if (d.GrossWeight.HasValue && d.NetWeight.HasValue)
                        return d.NetWeight.Value <= d.GrossWeight.Value;
                    return true;
                })
                .WithMessage("NetWeight cannot be greater than GrossWeight.");
        }
    }
}
