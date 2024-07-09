namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        Task SendResetPasswordMailAsync(string to, Guid userId, string resetToken);
        Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string name, string surname);
    }
}
