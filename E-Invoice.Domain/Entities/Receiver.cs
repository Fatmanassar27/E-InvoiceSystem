using System.ComponentModel.DataAnnotations;

namespace E_Invoice.Domain.Entities
{
    public class Receiver : Party
    {
        [Required(ErrorMessage = "Address is required.")]
        public virtual ReceiverAddress Address { get; set; }
    }
}
