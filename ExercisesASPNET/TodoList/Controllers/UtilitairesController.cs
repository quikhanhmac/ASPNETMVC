﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers
{
    public class UtilitairesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}