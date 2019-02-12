using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uploader.Models
{
    public class FileUploadModel
    {
        public IFormFile File { get; set; }
    }
}
