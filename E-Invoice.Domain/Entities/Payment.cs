namespace E_Invoice.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public string? BankName { get; set; }
        public string? BankAddress { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankAccountIBAN { get; set; }
        public string? SwiftCode { get; set; }
        public string? Terms { get; set; }
    }
}
