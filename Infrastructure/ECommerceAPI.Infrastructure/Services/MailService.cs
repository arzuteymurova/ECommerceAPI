using ECommerceAPI.Application.Abstractions.Services;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ECommerceAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();

            foreach (var to in tos)
                mail.To.Add(to);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;
            mail.From = new(_configuration["MailSettings:Username"], "E-Commerce", Encoding.UTF8);

            SmtpClient smtpClient = new();
            smtpClient.Credentials = new NetworkCredential(_configuration["MailSettings:Username"], _configuration["MailSettings:Password"]);
            smtpClient.Host = _configuration["MailSettings:Host"];
            smtpClient.Port = int.Parse(_configuration["MailSettings:Port"]);
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mail);
        }

        public async Task SendResetPasswordMailAsync(string to, Guid userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.Append("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
            mail.Append(_configuration["AngularClientUrl"]);
            mail.Append("/update-password/");
            mail.Append(userId.ToString());
            mail.Append("/");
            mail.Append(resetToken);
            mail.Append("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br>NG - Mini|E-Ticaret");

            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());
        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string name, string surname)
        {
            string mail = $"Dear {name} {surname}<br>Your order with code {orderCode}, which you placed on {orderDate}, has been completed and given to the cargo company.<br>Good luck!";

            await SendMailAsync(to, "Order completed", mail);
        }
    }
}
