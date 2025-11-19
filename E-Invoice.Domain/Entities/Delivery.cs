namespace E_Invoice.Domain.Entities
{
    public class Delivery
    {
        public int Id { get; set; }
        public string? Approach { get; set; }
        public string? Packaging { get; set; }
        public DateTime? DateValidity { get; set; }
        public string? ExportPort { get; set; }
        public string? CountryOfOrigin { get; set; }
        public decimal? GrossWeight { get; set; }
        public decimal? NetWeight { get; set; }
        public string? Terms { get; set; }
    }
}
