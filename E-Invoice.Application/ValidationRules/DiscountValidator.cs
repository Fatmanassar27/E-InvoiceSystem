using E_Invoice.Application.Validators.Extention;

namespace E_Invoice.Application.Validators
{
    public class DiscountValidator : AbstractValidator<DiscountDto>
    {
        public DiscountValidator()
        {
            RuleFor(d => d.Rate)
                        .InclusiveBetween(0m, 100m)
                        .When(d => d.Rate.HasValue)
                        .WithMessage("Rate must be between 0 and 100.")
                        .HasMaxDecimalPlaces(5);

            RuleFor(d => d.Amount)
           .HasMaxDecimalPlaces(5)
           .When(d => d.Amount.HasValue)
           .WithMessage("Amount allows up to 5 decimal digits.");

            When(d => d.Rate.HasValue && d.Rate.Value > 0, () =>
            {
                RuleFor(d => d.Amount)
                    .NotNull()
                    .WithMessage("Amount is required when Rate is provided and greater than 0.");
            });
            
            When(d => (d.Rate is null || d.Rate == 0) && d.Amount.HasValue , () =>
            {
                RuleFor(d => d.Amount)
                    .Null()
                    .WithMessage("Amount must equal 0 when Rate equal 0.");
            });

        }


    }

}

