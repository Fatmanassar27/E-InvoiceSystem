using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Invoice.Application.DTOs
{
    public class DiscountDto
    {
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
    }
}
