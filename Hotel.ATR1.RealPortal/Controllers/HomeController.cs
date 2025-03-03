using Hotel.ATR1.RealPortal.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace Hotel.ATR1.RealPortal.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEnumerable<IMessage> _message;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ILogger<HomeController> logger, 
            IEnumerable<IMessage> message, 
            IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _message = message;
            _localizer = localizer;
        }

        public IActionResult Contact()
        {
            Response.Cookies.Delete("city");

            return View();
        }

        [HttpPost]
        public IActionResult AddMessage(Message userMessage)
        {
            _logger.LogInformation("шаг 1. ({method}) Попытка отправки сообщения", "AddMessage");
            _logger.LogInformation("шаг 2. ({method}) Проверка валидности формы", "AddMessage");
            if (ModelState.IsValid)
            {
                _logger.LogError("шаг 3. ({method}) Форма корректная", "AddMessage");
            }

            _logger.LogInformation("шаг 4. ({method}) уведомления пользователю {user}",
                "AddMessage", userMessage.name);
            foreach (var item in _message)
            {
                item.SendMessage(userMessage.email, userMessage.message);
            }

            return RedirectToAction("Contact", "home");
        }

        //[HttpGet]
        public IActionResult Index()
        {
            _logger.LogCritical("Index > LogCritical");
            _logger.LogError("Index > LogError");
            _logger.LogWarning("Index > LogWarning");
            _logger.LogInformation("Index > LogInformation");

            return View();
        }

        //[HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        public JsonResult SetCity(string city)
        {
            try
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMilliseconds(100000);

                Response.Cookies.Append("city", city, options);
                return Json(city);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
