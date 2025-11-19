using E_Invoice.Domain.Entities.identity;

namespace E_Invoice.Application.Interfaces.IIdentity
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task CreateAsync(ApplicationUser user);
    }

}
