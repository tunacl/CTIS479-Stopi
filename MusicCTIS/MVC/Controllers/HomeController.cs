﻿using Microsoft.AspNetCore.Mvc;
using MVC.Controllers.Bases;
using MVC.Models;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : MvcControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
