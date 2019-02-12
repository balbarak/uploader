using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uploader.Models;

namespace Uploader.Controllers
{
    public class UploadController : Controller
    {
        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Index(FileUploadModel model)
        {
            return Ok(model.File.Length);
        }
    }
}