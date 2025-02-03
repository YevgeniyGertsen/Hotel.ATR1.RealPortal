using Hotel.ATR1.RealPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotel.ATR1.RealPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        //[HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult AddMessage(string name, string email, string message)
        public IActionResult AddMessage(Message userMessage)
        {
            var data = Request.Form;
            //var data = Request.Form["name"];

            return RedirectToAction("Contact","home");
            //return View();
        }
    }
}
