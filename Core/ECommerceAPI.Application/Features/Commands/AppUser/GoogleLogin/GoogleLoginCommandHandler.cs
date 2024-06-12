using ECommerceAPI.Application.Abstractions.Identity;
using ECommerceAPI.Infrastructure.Services.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly JWTOptions _jwtSettings;
        readonly IJWTTokenService _jwtTokenService;

        public GoogleLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, IOptionsSnapshot<JWTOptions> jwtSettings, IJWTTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { "399408454585-734fad27vt56c5ipslhb9qfhldojj9qt.apps.googleusercontent.com" }
            };

            // Validate the Google token
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
            var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

            // Find the user based on external login info
            Domain.Entities.Identity.AppUser user;
            try
            {
                user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Multiple users found with the same external login info.");
            }

            if (user == null)
            {
                // Find the user based on email
                try
                {
                    //user = await _userManager.FindByEmailAsync(payload.Email);
                }
                catch (InvalidOperationException)
                {
                    throw new Exception("Multiple users found with the same email.");
                }

                if (user == null)
                {
                    // Create a new user if no user exists with the given email
                    user = new Domain.Entities.Identity.AppUser
                    {
                        Id = Guid.NewGuid(),
                        Email = payload.Email,
                        UserName = payload.Email
                    };

                    var identityResult = await _userManager.CreateAsync(user);
                    if (!identityResult.Succeeded)
                    {
                        throw new Exception("User creation failed: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                    }
                }

                // Add external login for the new or existing user
                var addLoginResult = await _userManager.AddLoginAsync(user, info);
                if (!addLoginResult.Succeeded)
                {
                    throw new Exception("Adding external login failed: " + string.Join(", ", addLoginResult.Errors.Select(e => e.Description)));
                }
            }

            // Generate and return the access token
            return new GoogleLoginCommandResponse
            {
                AccessToken = _jwtTokenService.GenerateJwt(_jwtSettings)
            };
        }

    }
}
