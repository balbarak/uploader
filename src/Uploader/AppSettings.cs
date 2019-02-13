using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Uploader
{
    public class AppSettings
    {
        public static IConfiguration Configuration { get; set; }

        public static string WebRootPath { get; set; }

        public static string DataFolderName
        {
            get
            {
                return Configuration.GetSection("Storage:FolderName").Value;
            }
        }

        public static string DataFolderPath
        {
            get
            {
                return Path.Combine(WebRootPath,DataFolderName);
            }
        }
    }
}
