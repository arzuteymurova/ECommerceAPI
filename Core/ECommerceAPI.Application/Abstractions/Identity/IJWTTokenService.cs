using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Infrastructure.Services.Identity;

namespace ECommerceAPI.Application.Abstractions.Identity
{
    public interface IJWTTokenService
    {
        Token GenerateAccessToken(JWTOptions jwtSettings, AppUser user);
        string GenerateRefreshToken();
    }
}
