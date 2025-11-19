using E_Invoice.Application.Interfaces.IProviders;
using E_Invoice.Domain.Entities;

namespace E_Invoice.Infrastructure.Providers
{
    public class TaxableTypeProvider : JsonDataProvider<BaseType> , ITaxableTypeProvider
    {
        public TaxableTypeProvider(string jsonFilePath) : base(jsonFilePath)
        {
        }
    }
}
