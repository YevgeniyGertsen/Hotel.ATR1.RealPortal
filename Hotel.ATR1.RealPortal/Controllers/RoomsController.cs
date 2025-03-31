using Microsoft.AspNetCore.Mvc;

namespace Hotel.ATR1.RealPortal.Controllers
{
    //[Route("hotel/room")]
    public class RoomsController : Controller
    {
        //[HttpGet("")]
        //[HttpGet("Index")]
        //[HttpGet("TopRooms")]
        public IActionResult Index()
        {
            ViewBag.NamePage = "room - grid view";
            ViewBag.DescriptionPage = "A quality room of hotel ATR with sea or mountain view";

            return View();
        }

        [HttpGet("{roomId}")]
        public IActionResult RoomDetails(int roomId)
        {
            return View();
        }

        [HttpGet("AllRooms/{status=available}")]
        public IActionResult RoomList(string status) 
        {

            return View();
        }
    }
}
