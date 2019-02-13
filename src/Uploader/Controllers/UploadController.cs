using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Uploader.Helpers;
using Uploader.Models;

namespace Uploader.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnv;

        public string DataFolderPath => Path.Combine(_hostingEnv.WebRootPath, AppSettings.DataFolderName);

        public UploadController(IHostingEnvironment env)
        {
            _hostingEnv = env;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Index(IFormFile file,FileUploadModel model)
        {
            JsonResultObject result = new JsonResultObject
            {
                RedirectUrl = Url.Action("")
            };

            try
            {
                CheckDir();

                var fileName = GenerateRandomFileName();
                var extension = GetFileExtension(file.FileName);
                var fileFullNamePath = Path.Combine(DataFolderPath, fileName);

                model.ContentType = file.ContentType;
                model.Extension = extension;

                var fileData = JsonConvert.SerializeObject(model);

                System.IO.File.WriteAllText($"{fileFullNamePath}.json", fileData);


                using (FileStream fs = new FileStream($"{fileFullNamePath}{extension}",FileMode.CreateNew,FileAccess.ReadWrite))
                {
                    var stream = file.OpenReadStream();

                    int bufferSize = 8096;

                    Span<byte> buffer = new byte[bufferSize];

                    int read = 1;

                    while (read > 0)
                    {
                        read = stream.Read(buffer);

                        ReadOnlySpan<byte> readSpan = buffer;

                        fs.Write(readSpan);
                    }
                }
                
                
            }
            catch (Exception ex)
            {

                return BadRequest(result);
            }


            return Ok(result);
        }


        private void CheckDir()
        {
            if (!Directory.Exists(DataFolderPath))
                Directory.CreateDirectory(DataFolderPath);
        }

        private string GenerateRandomFileName()
        {
            if (Directory.Exists(DataFolderPath))
            {
                var files = Directory.GetFiles(DataFolderPath);

                var fileInfoes = files.Select(a => new FileInfo(a));

                var candidateFileName = RandomHelper.GenerateFileName();

                var found = fileInfoes.Where(a => a.Name.Contains(candidateFileName)).FirstOrDefault();

                while (found != null)
                {
                    candidateFileName = RandomHelper.GenerateFileName();

                    found = fileInfoes.Where(a => a.Name.Contains(candidateFileName)).FirstOrDefault();
                }

                return candidateFileName;
            }
            else
                return RandomHelper.GenerateFileName();
        }

        private string GetFileExtension(string fileName)
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