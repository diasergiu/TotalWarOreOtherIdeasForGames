using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalWarOreOtherIdeasForGames.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }
        public IActionResult Index()
        {
            logger.LogInformation("You are at home");
            ViewBag.Name = "tank you for looing at our site  " + this.User.Identity.AuthenticationType;
            return View();
        }
        [Route("Index/{message}")]
        public IActionResult Index(string message)
        {
            logger.LogInformation("unatorised " + message);
            ViewBag.Worning = message;
            return View();
        }
    }
}
