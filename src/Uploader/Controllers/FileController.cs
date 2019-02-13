using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Uploader.Controllers
{
    public class FileController : Controller
    {
        [Route("d/{id}")]
        public IActionResult Index(string id)
        {
            return View();
        }
    }
}
