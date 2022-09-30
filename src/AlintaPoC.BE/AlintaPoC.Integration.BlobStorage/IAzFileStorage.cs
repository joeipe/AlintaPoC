using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Integration.BlobStorage
{
    public interface IAzFileStorage
    {
        Task UploadFileAsync(string containerName, Stream fileStream, string blobname);
        Task<IEnumerable<BlobItem>> ListFileBlobsAsync(string containerName, string prefix = null);
        Task<BlobClient> GetCloudBlockBlobAsync(string containerName, string blobname);
        Task DownloadFileAsync(BlobClient blob, Stream targetStream);
        Task DeleteFileAsync(BlobClient blob);
    }
}
