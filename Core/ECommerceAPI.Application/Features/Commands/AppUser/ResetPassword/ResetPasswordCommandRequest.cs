using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.ResetPassword
{
    public class ResetPasswordCommandRequest : IRequest<ResetPasswordCommandResponse>
    {
        public string Email { get;  set; }
    }
}