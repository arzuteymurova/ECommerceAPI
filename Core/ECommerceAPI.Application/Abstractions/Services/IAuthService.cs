using ECommerceAPI.Application.Abstractions.Services.Authentications;
using ECommerceAPI.Application.DTOs;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IAuthService : IInternalAuthentication, IExternalAuthentication
    {
        Task ResetPasswordAsync(string email);
        Task<bool> VerifyResetTokenAsync(Guid userId, string resetToken);
    }
}
