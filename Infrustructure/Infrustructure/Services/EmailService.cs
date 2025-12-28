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

        public async Task SendProductCreatedNotificationAsync(string productName, string recipientEmail, CancellationToken cancellationToken = default)
        {
            var subject = "New product created";
            var body = $@"
            <html>
                <body>
                    <h2>New Product Added</h2>
                    <p>A new product has been created in the system:</p>
                    <p><strong>{productName}</strong></p>
                    <p>Thank you for using our system!</p>
                </body>
            </html>";

            await SendEmailAsync(recipientEmail, subject, body, cancellationToken);
        }

        public async Task SendProductOutOfStockNotificationAsync(string productName, string recipientEmail, CancellationToken cancellationToken = default)
        {
            var subject = "Product Out of Stock Alert";
            var body = $@"
            <html>
                <body>
                    <h2>Stock Alert</h2>
                    <p>The following product is now out of stock:</p>
                    <p><strong>{productName}</strong></p>
                    <p>Please reorder as soon as possible.</p>
                </body>
            </html>";

            await SendEmailAsync(recipientEmail, subject, body, cancellationToken);
        }
    }
}
