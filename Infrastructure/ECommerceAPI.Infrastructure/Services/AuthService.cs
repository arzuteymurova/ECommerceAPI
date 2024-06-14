using ECommerceAPI.Application.Abstractions.Identity;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Infrastructure.Services.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
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

        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, IJWTTokenService jwtTokenService, IOptionsSnapshot<JWTOptions> jwtSettings, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
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
                string accessToken = _jwtTokenService.GenerateJwt(_jwtSettings);
                return new()
                {
                    AccessToken = accessToken,
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
                string token = _jwtTokenService.GenerateJwt(_jwtSettings);

                return new LoginUserResponse()
                {
                    AccessToken = token
                };
            }
            throw new AuthenticationErrorException();
        }
    }
}
