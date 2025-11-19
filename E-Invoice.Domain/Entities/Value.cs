namespace E_Invoice.Domain.Entities
{
    public class Value
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "CurrencySold is required.")]
        [StringLength(3, ErrorMessage = "CurrencySold must be a valid ISO 4217 currency code.")]
        public string CurrencySold { get; set; } = "EGP";
        [Required(ErrorMessage = "AmountEGP is required.")]
        public decimal AmountEGP { get; set; }
        public decimal? AmountSold { get; set; }
        public decimal? CurrencyExchangeRate { get; set; }
    }
}
