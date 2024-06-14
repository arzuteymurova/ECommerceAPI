using ECommerceAPI.Application.Abstractions.Services.Authentications;
using ECommerceAPI.Application.DTOs.User;
using MediatR;


namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IInternalAuthentication _authService;

        public LoginUserCommandHandler(IInternalAuthentication authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            LoginUserResponse loginUserResponse = await _authService.LoginAsync(request.UsernameOrEmail, request.Password);

            return new()
            {
                AccessToken = loginUserResponse.AccessToken,
            };
        }
    }
}
