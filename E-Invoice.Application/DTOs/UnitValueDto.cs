namespace E_Invoice.Application.DTOs
{
    public class UnitValueDto
    {
        [Required(ErrorMessage = "CurrencySold is required.")]
        [StringLength(3, ErrorMessage = "CurrencySold must be a valid ISO 4217 currency code.")]
        public string CurrencySold { get; set; }
        public decimal AmountEGP { get; set; }
        public decimal AmountSold { get; set; }
        public decimal CurrencyExchangeRate { get; set; }
    }
}
