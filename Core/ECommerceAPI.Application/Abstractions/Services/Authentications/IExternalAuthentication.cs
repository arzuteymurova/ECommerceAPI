using ECommerceAPI.Application.DTOs.User;

namespace ECommerceAPI.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<LoginUserResponseDto> GoogleLoginAsync(string authToken);
    }
}
