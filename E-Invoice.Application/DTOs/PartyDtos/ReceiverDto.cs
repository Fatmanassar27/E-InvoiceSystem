namespace E_Invoice.Application.DTOs.PartyDtos
{
    public class ReceiverDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
    }
}
