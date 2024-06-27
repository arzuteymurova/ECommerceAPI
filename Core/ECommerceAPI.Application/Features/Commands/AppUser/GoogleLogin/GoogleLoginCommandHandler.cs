using ECommerceAPI.Application.Abstractions.Services.Authentications;
using ECommerceAPI.Application.DTOs.User;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IExternalAuthentication _authService;

        public GoogleLoginCommandHandler(IExternalAuthentication authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            LoginUserResponseDto loginUserResponse = await _authService.GoogleLoginAsync(request.IdToken);

            return new()
            {
                Token = loginUserResponse.Token,
            };
        }

    }
}
