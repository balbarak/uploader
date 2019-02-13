using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Uploader.Helpers;
using Uploader.Models;

namespace Uploader.Controllers
{
    public class FileController : Controller
    {
        [Route("doc/{id}")]
        public IActionResult Index(string id)
        {
            var model = GetFileModel(id);

            if (model == null)
                return View("NotFound");

            return View(model);
        }


        [Route("file/{id}")]
        public IActionResult Download(string id)
        {
            var model = GetFileModel(id);

            if (model == null)
                return NotFound();

            var filePath = Path.Combine(AppSettings.DataFolderPath, $"{id}{model.Extension}");

            var fileBytes = System.IO.File.ReadAllBytes(filePath);


            return File(fileBytes, model.ContentType, true);
        }


        public IActionResult Recents()
        {
            var files = GetRecentFiles();

            return View(files);
        }

        private FileUploadModel GetFileModel(string fileName)
        {
            fileName += ".json";

            var fileNamePath = Path.Combine(AppSettings.DataFolderPath, fileName);

            if (!System.IO.File.Exists(fileNamePath))
                return null;

            var json = System.IO.File.ReadAllText(fileNamePath);

            var result = JsonConvert.DeserializeObject<FileUploadModel>(json);

            result.Id = fileName.Replace(".json", "");

            return result;
        }

        private List<FileUploadModel> GetRecentFiles()
        {
            List<FileUploadModel> result = new List<FileUploadModel>();

            if (!Directory.Exists(AppSettings.DataFolderPath))
                return result;

            var files = Directory.GetFiles(AppSettings.DataFolderPath);

            var fileInfos = files.Select(a => new FileInfo(a)).ToList();

            fileInfos = fileInfos.Where(a =>
            a.Name.Contains(".json"))
            .OrderByDescending(a => a.CreationTimeUtc)
            .Take(10)
            .ToList();

            foreach (var item in fileInfos)
            {
                result.Add(new FileUploadModel()
                {
                    Id = item.Name.Replace(".json",""),
                    Extension = item.Extension,
                });
            }

            return result;
        }

    }
}
