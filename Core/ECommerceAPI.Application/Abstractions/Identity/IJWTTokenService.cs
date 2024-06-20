using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Infrastructure.Services.Identity;

namespace ECommerceAPI.Application.Abstractions.Identity
{
    public interface IJWTTokenService
    {
        Token GenerateAccessToken(JWTOptions jwtSettings);
        string GenerateRefreshToken();
    }
}
