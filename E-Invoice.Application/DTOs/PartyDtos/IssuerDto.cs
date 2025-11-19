namespace E_Invoice.Application.DTOs.PartyDtos
{
    public class IssuerDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
    }
}
