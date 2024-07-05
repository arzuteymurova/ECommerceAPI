using ECommerceAPI.Application.Abstractions.Services;
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

        public async Task SendResetPasswordMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("If you want to reset your password, click the below link.<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\"></a></strong><br><br><span style=\"font-size:12px;\"><br><br><br>E-Commerce");

            await SendMailAsync(to, "Reset Password", mail.ToString());
        }
    }
}
