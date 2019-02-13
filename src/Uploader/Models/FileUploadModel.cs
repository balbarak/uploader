using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uploader.Models
{
    public class FileUploadModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public string ContentType { get; set; }

        public string Markdown { get; set; }

        public FileUploadModel()
        {

        }

        public FileUploadModel(string extension, string contentType)
        {
            Extension = extension;

            ContentType = contentType;
        }

    }
}
