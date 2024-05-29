using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Services
{
    public interface IFileService
    {
        Task UploadAsync(string path, IFormFileCollection files);
        Task<string> FileRenameAsync(string fileName);
        Task<bool> SaveAsync(string path, IFormFile file);
    }
}
