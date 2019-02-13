using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Uploader.Models;

namespace Uploader.Controllers
{
    public class FileController : Controller
    {
        [Route("d/{id}")]
        public IActionResult Index(string id)
        {
            var model = GetFileModel(id);

            if (model == null)
                return View("NotFound");

            return View(model);
        }


        [Route("f/{id}")]
        public IActionResult Download(string id)
        {
            var model = GetFileModel(id);

            if (model == null)
                return View("NotFound");

            return View(model);
        }


        private FileUploadModel GetFileModel(string fileName)
        {
            fileName += ".json";

            var fileNamePath = Path.Combine(AppSettings.DataFolderPath,fileName);

            if (!System.IO.File.Exists(fileNamePath))
                return null;

            var json = System.IO.File.ReadAllText(fileNamePath);

            var result = JsonConvert.DeserializeObject<FileUploadModel>(json);

            result.Id = fileName.Replace(".json","");

            return result;
        }

    }
}
