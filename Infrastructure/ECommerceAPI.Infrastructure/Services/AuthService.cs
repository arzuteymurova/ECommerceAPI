using ECommerceAPI.Application.Abstractions.Identity;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Infrastructure.Services.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ECommerceAPI.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IJWTTokenService _jwtTokenService;
        readonly JWTOptions _jwtSettings;
        readonly IUserService _userService;

        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, IJWTTokenService jwtTokenService, IOptionsSnapshot<JWTOptions> jwtSettings, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _userService = userService;
        }

        private async Task<LoginUserResponse> CreateExternalUserAsync(AppUser user, string email, string firstName, string lastName, UserLoginInfo info)
        {
            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid(),
                        Email = email,
                        UserName = email,
                        FirstName = firstName,
                        LastName = lastName,
                    };

                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
            {
                var addLoginResult = await _userManager.AddLoginAsync(user, info);
                Token token = _jwtTokenService.GenerateAccessToken(_jwtSettings,user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes), _jwtSettings.RefreshTokenLifeTime);

                return new()
                {
                    Token = token,
                };
            }

            throw new Exception("Invalid external authentication.");
        }


        public async Task<LoginUserResponse> GoogleLoginAsync(string authToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:ClientId"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(authToken, settings);
            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateExternalUserAsync(user, payload.Email, payload.GivenName, payload.FamilyName, info);
        }

        public async Task<LoginUserResponse> LoginAsync(string usernameOrEmail, string password)
        {
            AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);

            user ??= await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                Token token = _jwtTokenService.GenerateAccessToken(_jwtSettings, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes), _jwtSettings.RefreshTokenLifeTime);

                return new LoginUserResponse()
                {
                    Token = token
                };
            }
            throw new AuthenticationErrorException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _jwtTokenService.GenerateAccessToken(_jwtSettings, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes), _jwtSettings.RefreshTokenLifeTime);
                return token;
            }
            else
                throw new NotFoundUserException();
        }
    }
}
