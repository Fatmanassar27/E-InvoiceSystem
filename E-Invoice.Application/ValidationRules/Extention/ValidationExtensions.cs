using FluentValidation;

namespace E_Invoice.Application.Validators.Extention
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, decimal> HasMaxDecimalPlaces<T>(
            this IRuleBuilder<T, decimal> rule, int maxDecimalPlaces)
        {
            return rule.Must(value =>
            {
                var parts = value.ToString().Split('.');
                return parts.Length == 1 || parts[1].Length <= maxDecimalPlaces;
            })
            .WithMessage($"Value must have at most {maxDecimalPlaces} decimal places.");
        }

        public static IRuleBuilderOptions<T, decimal?> HasMaxDecimalPlaces<T>(
            this IRuleBuilder<T, decimal?> rule, int maxDecimalPlaces)
        {
            return rule.Must(value =>
            {
                if (!value.HasValue)
                    return true;

                var text = value.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                var parts = text.Split('.');
                return parts.Length == 1 || parts[1].Length <= maxDecimalPlaces;
            })
            .WithMessage($"Value must have at most {maxDecimalPlaces} decimal places.");
        }
    }
}
