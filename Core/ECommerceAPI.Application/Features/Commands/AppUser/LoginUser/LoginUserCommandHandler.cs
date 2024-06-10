using MediatR;
using Microsoft.AspNetCore.Identity;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Abstractions.Identity;
using Microsoft.Extensions.Options;
using ECommerceAPI.Infrastructure.Services.Identity;


namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly JWTOptions _jwtSettings;
        readonly IJWTTokenService _jwtTokenService;

        public LoginUserCommandHandler(SignInManager<Domain.Entities.Identity.AppUser> signInManager,
            UserManager<Domain.Entities.Identity.AppUser> userManager, IOptionsSnapshot<JWTOptions> jwtSettings, IJWTTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);

            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                string token = _jwtTokenService.GenerateJwt(_jwtSettings);
                return new LoginUserCommandResponse()
                {
                    Token = token
                };

            }
            throw new NotFoundUserException();
        }
    }
}
