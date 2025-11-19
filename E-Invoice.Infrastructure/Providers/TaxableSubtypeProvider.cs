using E_Invoice.Application.Interfaces.IProviders;
using E_Invoice.Domain.Entities;
namespace E_Invoice.Infrastructure.Providers
{
    public class TaxableSubtypeProvider : JsonDataProvider<BaseSubType>, ITaxableSubtypeProvider
    {
        public TaxableSubtypeProvider(string jsonFilePath) : base(jsonFilePath)
        {
        }
    }
}
