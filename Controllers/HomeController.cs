using CAFFEINE.Data;
using CAFFEINE.Models;
using CAFFEINE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CAFFEINE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CaffService _caffService;

        public HomeController(ILogger<HomeController> logger,CaffService caffService)
        {
            _logger = logger;
            _caffService = caffService;

        }

        public IActionResult Index()
        {
            var caff = _caffService.ParseCaff();
            List<Caff> caffList = new List<Caff>() { caff };
            return View(caffList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
