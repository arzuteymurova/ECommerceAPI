using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.VerifyResetToken
{
    public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenCommandRequest, VerifyResetTokenCommandResponse>
    {
        private readonly IAuthService _authService;

        public VerifyResetTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<VerifyResetTokenCommandResponse> Handle(VerifyResetTokenCommandRequest request, CancellationToken cancellationToken)
        {
            bool state = await _authService.VerifyResetTokenAsync(request.UserId, request.ResetToken);
            return new()
            {
                State = state,
            };
        }
    }
}
