using Microsoft.AspNetCore.Mvc;

namespace Hotel.ATR1.RealPortal.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.NamePage = "room - grid view";
            ViewBag.DescriptionPage = "A quality room of hotel ATR with sea or mountain view";

            return View();
        }
    }
}
