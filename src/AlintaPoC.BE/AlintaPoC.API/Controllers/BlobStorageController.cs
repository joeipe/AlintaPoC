using AlintaPoC.Integration.BlobStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlintaPoC.Contracts;

namespace AlintaPoC.API.Controllers
{
    public class BlobStorageController : CommonController
    {
        private readonly ILogger<BlobStorageController> _logger;
        private readonly IAzFileStorage _fileStorage;

        public BlobStorageController(
            ILogger<BlobStorageController> logger,
            IAzFileStorage fileStorage)
        {
            _logger = logger;
            _fileStorage = fileStorage;
        }

        [HttpGet()]
        public async Task<ActionResult> ListFiles(string containerName, string prefix)
        {
            var vm = await _fileStorage.ListFileBlobsAsync(containerName, prefix).ConfigureAwait(false);

            return Response(vm.ToList().Select(x => new
            {
                Name = x.Name,
                CreatedOn = x.Properties.CreatedOn,
                Topic = x.Name.Split('/')[1],
            }).OrderByDescending(x => x.CreatedOn));
        }

        [HttpPost()]
        public async Task<ActionResult> DownloadJsonFile([FromBody] DownloadFileReqDto request)
        {
            try
            {
                var blob = await _fileStorage.GetCloudBlockBlobAsync(request?.ContainerName, request?.BlobName).ConfigureAwait(false);

                var stream = new MemoryStream();
                await _fileStorage.DownloadFileAsync(blob, stream).ConfigureAwait(false);

                var jsonString = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);

                return Ok(jsonString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost()]
        public async Task<FileResult> DownloadExcelFile([FromBody] DownloadFileReqDto request)
        {
            try
            {
                var blob = await _fileStorage.GetCloudBlockBlobAsync(request?.ContainerName, request?.BlobName).ConfigureAwait(false);

                var stream = new MemoryStream();
                await _fileStorage.DownloadFileAsync(blob, stream).ConfigureAwait(false);

                var val = System.Convert.ToBase64String(stream.ToArray());

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesDataExtract.xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("DeleteFile")]
        public async Task<ActionResult> DeleteFile([FromBody] DownloadFileReqDto request)
        {
            var blob = await _fileStorage.GetCloudBlockBlobAsync(request?.ContainerName, request?.BlobName).ConfigureAwait(false);

            await _fileStorage.DeleteFileAsync(blob).ConfigureAwait(false);

            return Ok();
        }
    }
}
