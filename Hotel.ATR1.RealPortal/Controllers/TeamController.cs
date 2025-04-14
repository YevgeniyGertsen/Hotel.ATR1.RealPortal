using Hotel.ATR1.RealPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hotel.ATR1.RealPortal.Controllers
{
    public class TeamController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Team> teams = new List<Team>();
            using (var client = new HttpClient())
            {
                using (var responce = await client.GetAsync("http://localhost:5288/api/Team/getAllTeam"))
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    teams = JsonConvert.DeserializeObject<List<Team>>(result);
                }
            }

            return View(teams);
        }
    }
}