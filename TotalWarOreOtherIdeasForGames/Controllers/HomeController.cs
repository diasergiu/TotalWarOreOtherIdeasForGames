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
            return View();
        }
        [Route("Index/{message}")]
        public IActionResult Index(string message)
        {
            logger.LogInformation("unatorised " + message);
            return View();
        }
    }
}
