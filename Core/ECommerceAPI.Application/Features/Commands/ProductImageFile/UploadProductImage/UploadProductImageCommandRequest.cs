using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandRequest:IRequest<UploadProductImageCommandResponse>
    {
        public Guid Id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
