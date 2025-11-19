using E_Invoice.Application.Interfaces.IProviders;
using E_Invoice.Domain.Entities;

namespace E_Invoice.Infrastructure.Providers
{
    public class UnitTypeProvider : JsonDataProvider<BaseType>, IUnitTypeProvider
    {
        public UnitTypeProvider(string jsonFilePath) : base(jsonFilePath)
        {
        }
    }
}
