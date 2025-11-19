namespace E_Invoice.Application.DTOs
{
    public class PaymentDto
    {
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountIBAN { get; set; }
        public string SwiftCode { get; set; }
        public string Terms { get; set; }
    }
}
