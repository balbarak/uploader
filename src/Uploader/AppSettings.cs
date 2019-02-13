using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uploader
{
    public class AppSettings
    {
        public static IConfiguration Configuration { get; set; }

        public static string DataFolderName
        {
            get
            {
                return Configuration.GetSection("Storage:FolderName").Value;
            }
        }
    }
}
