using E_Invoice.Domain.Entities.identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace E_Invoice.Infrastructure.Identity
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
        {
        }
    }
}
