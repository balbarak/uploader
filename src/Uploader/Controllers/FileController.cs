﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Uploader.Controllers
{
    public class FileController : Controller
    {
        public IActionResult Index(string id)
        {
            return View();
        }
    }
}