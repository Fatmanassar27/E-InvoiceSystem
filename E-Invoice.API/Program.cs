using E_Invoice.Application.DTOs;
using E_Invoice.Application.DTOs.PartyDtos;
using E_Invoice.Application.Interfaces;
using E_Invoice.Application.Interfaces.IIdentity;
using E_Invoice.Application.Interfaces.IProviders;
using E_Invoice.Application.Mapping;
using E_Invoice.Application.Services;
using E_Invoice.Application.Services.identitiy_services;
using E_Invoice.Application.Validators;
using E_Invoice.Infrastructure.Configurations;
using E_Invoice.Infrastructure.Providers;
using E_Invoice.Infrastructure.Repositories.identity;
using E_Invoice.Infrastructure.Services;
using E_Invoice.Infrastructure.Services.identity;
using E_Invoice.Infrastructure.UnitOfWorks;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace E_Invoice.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Json Data Providers

            var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "../E-Invoice.Infrastructure/Data/Seeds");

            builder.Services.AddSingleton<IActivityTypeProvider>(
                new ActivityTypeProvider(Path.Combine(dataPath, "ActivityTypes.json"))
            );

            builder.Services.AddSingleton<ICountryCodeProvider>(
                new CountryCodeProvider(Path.Combine(dataPath, "CountryCodes.json"))
            );

            builder.Services.AddSingleton<IUnitTypeProvider>(
                new UnitTypeProvider(Path.Combine(dataPath, "UnitTypes.json"))
            );

            builder.Services.AddSingleton<ITaxableTypeProvider>(
                new TaxableTypeProvider(Path.Combine(dataPath, "TaxableTypes.json"))
            );

            builder.Services.AddSingleton<ITaxableSubtypeProvider>(
                new TaxableSubtypeProvider(Path.Combine(dataPath, "TaxableSubtypes.json"))
            );


            #endregion

            #region Infrastructure (DB Context etc.)
            builder.Services.AddInfrastructureServices(builder.Configuration);
            #endregion

            #region Validators

            // Validators that have dependencies on Providers must be registered AFTER Providers
            builder.Services.AddTransient<IValidator<TaxTotalDto>, TaxTotalValidator>();
            builder.Services.AddTransient<IValidator<TaxableItemDto>, TaxableItemValidator>();
            builder.Services.AddTransient<IValidator<InvoiceLineDto>, InvoiceLineValidator>();
            builder.Services.AddTransient<IValidator<InvoiceDto>, InvoiceValidator>();
            builder.Services.AddTransient<IValidator<AddressDto>, AddressValidator>();
            builder.Services.AddTransient<IValidator<DeliveryDto>, DeliveryValidator>();
            builder.Services.AddTransient<IValidator<ReceiverDto>, ReceiverValidator>();
            builder.Services.AddTransient<IValidator<UnitValueDto>, ValueValidator>();
            builder.Services.AddTransient<IValidator<DiscountDto>, DiscountValidator>();

            #endregion

            #region Controllers with FluentValidation
            builder.Services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<InvoiceValidator>();
                //fv.DisableDataAnnotationsValidation = true; 
            });

            #endregion

            #region Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\n\nExample: \"Bearer eyJhbGciOiJI...\""
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                });

            builder.Services.AddOpenApi();
            #endregion

            #region Services
            builder.Services.AddAutoMapper(typeof(InvoiceProfile).Assembly);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IDocumentSubmissionService, DocumentSubmissionService>();
            #endregion

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
