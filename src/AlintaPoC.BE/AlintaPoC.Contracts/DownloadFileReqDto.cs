using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Contracts
{
    public class DownloadFileReqDto
    {
        [Required]
        public string ContainerName { get; set; }
        [Required]
        public string BlobName { get; set; }
    }
}
