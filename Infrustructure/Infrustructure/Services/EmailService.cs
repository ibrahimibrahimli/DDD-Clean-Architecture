using Application.Common.Interfaces;
using Infrustructure.Settings;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace Infrustructure.Services
{
    public sealed class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly EmailSettings _emailSettings;

        public EmailService(ILogger<EmailService> logger, EmailSettings emailSettings)
        {
            _logger = logger;
            _emailSettings = emailSettings;
        }
        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            try
            {
                using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
                {
                    Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                    EnableSsl = _emailSettings.EnableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage, cancellationToken);

                _logger.LogInformation("Email sent successfully to {To} with subject {Subject}", to, subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {To} with subject {Subject}", to, subject);
                throw;
            }
        }

        public Task SendProductCreatedNotificationAsync(string productName, string recipientEmail, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SendProductOutOfStockNotificationAsync(string productName, string recipientEmail, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
