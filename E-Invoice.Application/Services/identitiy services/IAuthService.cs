using E_Invoice.Application.DTOs.Identity;

namespace E_Invoice.Application.Services.identitiy_services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(string email, string password);
        Task RegisterAsync(RegisterDto model);
    }

}
