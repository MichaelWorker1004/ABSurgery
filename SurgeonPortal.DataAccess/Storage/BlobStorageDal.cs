using Azure.Storage.Blobs;
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

        public BlobStorageDal(IOptions<AzureStorageConfiguration> options)
        {
            _storageConfiguration = options.Value;
            var blobServiceClient = new BlobServiceClient(_storageConfiguration.ConnectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient(_storageConfiguration.ContainerName);

        }

        public Task<Stream> LoadAsync(string name)
        {
            var blobClient = _containerClient.GetBlobClient(name);
            return blobClient.OpenReadAsync();
        }

        public Task SaveAsync(Stream fileStream, string name)
        {
            throw new NotImplementedException();
        }

    }
}
