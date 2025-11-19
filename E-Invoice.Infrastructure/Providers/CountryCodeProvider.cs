using E_Invoice.Application.Interfaces.IProviders;
using E_Invoice.Domain.Entities;

namespace E_Invoice.Infrastructure.Providers
{
    public class CountryCodeProvider : JsonDataProvider<BaseType>, ICountryCodeProvider
    {
        public CountryCodeProvider(string jsonFilePath) : base(jsonFilePath)
        {
        }
    }
}
