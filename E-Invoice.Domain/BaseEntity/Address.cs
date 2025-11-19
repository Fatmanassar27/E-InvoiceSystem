namespace E_Invoice.Domain.BaseEntity
{
    public abstract class Address
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Governate is required")]
        public string Governate { get; set; }
        [Required(ErrorMessage = "RegionCity is required")]
        public string RegionCity { get; set; }
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "BuildingNumber is required")]
        public string BuildingNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? Floor { get; set; }
        public string? Room { get; set; }
        public string? Landmark { get; set; }
        public string? AdditionalInformation { get; set; }
    }
}
