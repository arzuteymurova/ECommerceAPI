using ECommerceAPI.Application.Abstractions.Identity;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceAPI.Infrastructure.Services.Identity
{
    public class JWTTokenService : IJWTTokenService
    {
        public Token GenerateAccessToken(JWTOptions jwtSettings, AppUser user)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime accessTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtSettings.ExpirationInMinutes));


            JwtSecurityToken token = new(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: accessTokenExpiration,
                signingCredentials: credentials,
                claims: new List<Claim> { new(ClaimTypes.Name, user.UserName) }
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            string refreshToken = GenerateRefreshToken();

            return new Token()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public string GenerateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
