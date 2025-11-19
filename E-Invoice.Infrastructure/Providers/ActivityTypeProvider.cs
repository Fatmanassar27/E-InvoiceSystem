using E_Invoice.Application.Interfaces.IProviders;
using E_Invoice.Domain.Entities;
namespace E_Invoice.Infrastructure.Providers
{
    public class ActivityTypeProvider : JsonDataProvider<BaseType>, IActivityTypeProvider
    {
        public ActivityTypeProvider(string jsonFilePath) : base(jsonFilePath)
        {
        }
    }
}
