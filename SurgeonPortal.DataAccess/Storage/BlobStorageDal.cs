using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SurgeonPortal.DataAccess.Contracts.Storage;
using SurgeonPortal.Shared.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Storage
{
    public class BlobStorageDal : IStorageDal
    {
        private readonly AzureStorageConfiguration _storageConfiguration;
        private readonly BlobContainerClient _containerClient;
        private readonly ILogger<BlobStorageDal> _logger;

        public BlobStorageDal(IOptions<AzureStorageConfiguration> options,
            ILogger<BlobStorageDal> logger)
        {
            _storageConfiguration = options.Value;
            var blobServiceClient = new BlobServiceClient(_storageConfiguration.ConnectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient(_storageConfiguration.ContainerName);

            _logger = logger;

        }

        public Task DeleteAsync(string name)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(name);
            return blobClient.DeleteAsync();
        }

        public Task<Stream> LoadAsync(string name)
        {
            var blobClient = _containerClient.GetBlobClient(name);
            return blobClient.OpenReadAsync();
        }
        public Task SaveAsync(Stream fileStream, string name)
        {
            // Get the Blob Client used to interact with (including create) the blob
            BlobClient blobClient = _containerClient.GetBlobClient(name);

            // Upload the blob
            try
            {
                return blobClient.UploadAsync(fileStream);
            }
            catch (Exception ex)
            {
                _logger.LogError($"BlobStorageDal SaveAsync FAILED when trying to upload file to Azure Blob Storage. Error Message = {ex.Message}");
                return null;
            }
        }

    }
}
