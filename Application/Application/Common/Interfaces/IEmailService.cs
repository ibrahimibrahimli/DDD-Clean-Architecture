namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
        Task SendProductCreatedNotificationAsync(string productName, string recipientEmail, CancellationToken cancellationToken = default);
        Task SendProductOutOfStockNotificationAsync(string productName, string recipientEmail, CancellationToken cancellationToken = default);
    }
}
