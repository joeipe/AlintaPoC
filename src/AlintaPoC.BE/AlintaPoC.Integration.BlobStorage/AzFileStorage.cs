using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Runtime;

namespace AlintaPoC.Integration.BlobStorage
{
    public class AzFileStorage : IAzFileStorage
    {
        private readonly string _storageConnectionString;

        public AzFileStorage(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
        }

        public async Task UploadFileAsync(string containerName, Stream fileStream, string blobname)
        {
            var container = await GetCoreWrapperContainerAsync(containerName).ConfigureAwait(false);

            container.UploadBlob(blobname, fileStream);
        }

        public async Task<IEnumerable<BlobItem>> ListFileBlobsAsync(string containerName, string prefix = null)
        {
            var cloudBlobs = new List<BlobItem>();
            var container = await GetCoreWrapperContainerAsync(containerName).ConfigureAwait(false);

            foreach (BlobItem blob in container.GetBlobs(BlobTraits.None, BlobStates.None, prefix))
            {
                cloudBlobs.Add(blob);
            }

            return cloudBlobs;
        }

        public async Task<BlobClient> GetCloudBlockBlobAsync(string containerName, string blobname)
        {
            var container = await GetCoreWrapperContainerAsync(containerName).ConfigureAwait(false);

            BlobClient blob = container.GetBlobClient(blobname);

            return blob;
        }

        public async Task DownloadFileAsync(BlobClient blob, Stream targetStream)
        {
            if (blob == null)
                throw new ArgumentNullException(nameof(blob));

            await blob.DownloadToAsync(targetStream).ConfigureAwait(false);
        }

        public async Task DeleteFileAsync(BlobClient blob)
        {
            if (blob == null)
                throw new ArgumentNullException(nameof(blob));

            await blob.DeleteAsync().ConfigureAwait(false);
        }

        #region Private

        private async Task<BlobContainerClient> GetCoreWrapperContainerAsync(string containerName)
        {
            return await GetContainerAsync(containerName).ConfigureAwait(false);
        }

        private async Task<BlobContainerClient> GetContainerAsync(string containerName)
        {
            var container = new BlobContainerClient(_storageConnectionString, containerName);
            await container.CreateIfNotExistsAsync(PublicAccessType.Blob, null, null).ConfigureAwait(false);
            return container;
        }

        #endregion
    }
}
