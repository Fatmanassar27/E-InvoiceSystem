using E_Invoice.Application.Interfaces.IIdentity;
using E_Invoice.Domain.Entities.identity;
using E_Invoice.Infrastructure.Identity;

namespace E_Invoice.Infrastructure.Repositories.identity
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _context;

        public UserRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CreateAsync(ApplicationUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }

}