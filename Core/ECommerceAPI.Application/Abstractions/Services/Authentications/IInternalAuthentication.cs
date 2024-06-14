using ECommerceAPI.Application.DTOs.User;

namespace ECommerceAPI.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<LoginUserResponse> LoginAsync(string usernameOrEmail, string password);
    }
}
