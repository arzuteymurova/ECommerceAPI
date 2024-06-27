using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.DTOs.User;

namespace ECommerceAPI.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<LoginUserResponseDto> LoginAsync(string usernameOrEmail, string password);
        Task<Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
