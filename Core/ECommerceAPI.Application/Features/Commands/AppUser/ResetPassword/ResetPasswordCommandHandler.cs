using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, ResetPasswordCommandResponse>
    {
        private readonly IAuthService _authService;

        public ResetPasswordCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            await _authService.ResetPasswordAsync(request.Email);

            return new();
        }
    }
}
