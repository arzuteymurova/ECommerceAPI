using ECommerceAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;

namespace ECommerceAPI.Infrastructure.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        async Task<string> FileRenameAsync(string path, string fileName)
        {
            await Task.Run(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldFileName = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{NameOperation.CharacterRegulatory(oldFileName)}{extension}";

                if (File.Exists($"{path}\\{newFileName}"))
                    await FileRenameAsync(path, newFileName);
            });
            return "";
        }

    }
}