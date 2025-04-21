using Hotel.ATR1.RealPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace Hotel.ATR1.RealPortal.Controllers
{
    public class TeamController : Controller
    {
        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("_MySuperDuperMegaSecretKey_2025_Token!"));

            var credential = new SigningCredentials(securityKey, 
                SecurityAlgorithms.HmacSha256);

            var toke = new JwtSecurityToken(
                issuer: "http://satbayevproject.kz",
                audience: "http://satbayevproject.kz",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(toke);
        }

        public async Task<IActionResult> Index()
        {
            var jwt = GenerateToken();//Request.Cookies["jwtCookie"];

            List<Team> teams = new List<Team>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", jwt);

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