using System.Text;
using E_Invoice.Application.Interfaces.IIdentity;
using E_Invoice.Application.Interfaces;
using E_Invoice.Application.Mapping;
using E_Invoice.Application.Services.identitiy_services;
using E_Invoice.Application.Services;
using E_Invoice.Domain.Entities.identity;
using E_Invoice.Infrastructure.Data;
using E_Invoice.Infrastructure.Identity;
using E_Invoice.Infrastructure.Repositories.identity;
using E_Invoice.Infrastructure.Services.identity;
using E_Invoice.Infrastructure.Services;
using E_Invoice.Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace E_Invoice.Infrastructure.Configurations
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            #region dbcontext

            services.AddDbContext<EInvoiceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CS")));
            
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityCS")));
            #endregion

            #region Identity and Authentication
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddDefaultTokenProviders();
            
            var jwtSettings = configuration.GetSection("Jwt");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                };
            });

            #endregion

            #region Services
            services.AddAutoMapper(typeof(InvoiceProfile).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDocumentSubmissionService, DocumentSubmissionService>();
            services.AddScoped<IdentitySeeder>();
            #endregion

            return services;
        }
    }
}
