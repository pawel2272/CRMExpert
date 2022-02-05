using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.UI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HRBN.Thesis.CRMExpert.UI.Models;
using Microsoft.AspNetCore.Authorization;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ServiceFilter(typeof(JwtAuthFilter))]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}