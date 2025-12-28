using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrustructure.Services
{
    public sealed class LocalFileStorageService : IFileStorageService
    {
        readonly ILogger<LocalFileStorageService> _logger;
        readonly string _uploadPath;
        public LocalFileStorageService(ILogger<LocalFileStorageService> logger)
        {
            _logger = logger;
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default)
        {
            try
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
                var filePath = Path.Combine(_uploadPath, uniqueFileName);

                using var fileStreamOutput = new FileStream(filePath, FileMode.Create);
                await fileStreamOutput.CopyToAsync(fileStream, cancellationToken);

                _logger.LogInformation($"File uploaded successfully: {uniqueFileName}");
                return $"/uploads/{uniqueFileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload file: {FileName}", fileName);
                throw;
            }
        }

        public Task DeleteFileAsync(string fileUrl, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> DownloadFileAsync(string fileUrl, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FileExistsAsync(string fileUrl, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
