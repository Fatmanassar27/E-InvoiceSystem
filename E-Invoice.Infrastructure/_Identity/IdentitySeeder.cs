using E_Invoice.Domain.Entities.identity;
using E_Invoice.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace E_Invoice.Infrastructure.Identity
{
    public class IdentitySeeder
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public IdentitySeeder(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            foreach (var role in Enum.GetNames(typeof(RolesEnum)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new ApplicationRole { Name = role });
                }
            }
        }
    }
}
