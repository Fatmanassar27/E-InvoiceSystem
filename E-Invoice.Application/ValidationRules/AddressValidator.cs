using E_Invoice.Application.Interfaces.IProviders;

namespace E_Invoice.Application.Validators
{
    public class AddressValidator : AbstractValidator<AddressDto>
    {
        private readonly ICountryCodeProvider _CountryCodeProvider;
        public AddressValidator(ICountryCodeProvider CountryCodeProvider)
        {
            _CountryCodeProvider = CountryCodeProvider;
            RuleFor(address => address.Country)
                .NotEmpty().WithMessage("Country is required.")
                .Length(2).WithMessage("Country must be a 2-letter ISO-3166-2 code.")
                .Must(type => _CountryCodeProvider.GetAll().Any(t => t.Code == type))
                .When(t => !string.IsNullOrEmpty(t.Country))
                .WithMessage("Invalid Country code — not found in allowed list.");

        }
    }
}
