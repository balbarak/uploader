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

        public static string DataFolderPath
        {
            get
            {
                return Configuration.GetSection("FolderName").ToString();
            }
        }
    }
}
