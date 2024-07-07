using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.VerifyResetToken
{
    public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
    {
        public string ResetToken { get; set; }
        public Guid UserId { get; set; }
    }
}