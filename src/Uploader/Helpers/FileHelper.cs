using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Uploader.Helpers
{
    public class FileHelper
    {
        public static string GetExtension(string fileName)
        {
            var pattern = "\\.[^.\\/:*?\" <>|\r\n]+$";

            var regex = new Regex(pattern);

            var match = regex.Match(fileName);

            if (match.Success)
                return match.Value;

            return "";
        }
    }
}
