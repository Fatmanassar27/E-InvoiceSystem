using E_Invoice.Application.DTOs;

namespace E_Invoice.Application.Validators
{
    public class ValueValidator : AbstractValidator<UnitValueDto>
    {
        public ValueValidator()
        {
            RuleFor(v => v.AmountEGP)
                 .GreaterThan(0).WithMessage("AmountEGP must be greater than 0.")
                 .HasMaxDecimalPlaces(5).WithMessage("AmountEGP allows up to 5 decimal digits.");

            When(v => v.CurrencySold != null && v.CurrencySold.ToUpper() != "EGP", () =>
            {
                RuleFor(v => v.AmountSold)
                    .NotNull().WithMessage("AmountSold is required when CurrencySold is not EGP.")
                    .GreaterThan(0).WithMessage("AmountSold must be greater than 0.")
                    .HasMaxDecimalPlaces(5).WithMessage("AmountSold allows up to 5 decimal digits.");

                RuleFor(v => v.CurrencyExchangeRate)
                    .NotNull().WithMessage("CurrencyExchangeRate is required when CurrencySold is not EGP.")
                    .GreaterThan(0).WithMessage("CurrencyExchangeRate must be greater than 0.")
                    .HasMaxDecimalPlaces(5).WithMessage("CurrencyExchangeRate allows up to 5 decimal digits.");
            });
            When(v => v.CurrencySold != null && v.CurrencySold.ToUpper() == "EGP", () =>
            {
                RuleFor(v => v.AmountSold)
                    .Null().WithMessage("AmountSold must be null when CurrencySold is EGP.");

                RuleFor(v => v.CurrencyExchangeRate)
                    .Null().WithMessage("CurrencyExchangeRate must be null when CurrencySold is EGP.");
            });
        }
    }
}
