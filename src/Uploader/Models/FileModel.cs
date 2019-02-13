using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uploader.Models
{
    public class FileModel
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        public string ContentType { get; set; }

        public FileModel()
        {

        }

        public FileModel(string extension,string contentType)
        {
            Extension = extension;

            ContentType = contentType;
        }
    }
}
