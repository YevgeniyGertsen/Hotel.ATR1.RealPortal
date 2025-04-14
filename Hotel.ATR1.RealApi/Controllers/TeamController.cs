using Hotel.ATR1.RealApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.ATR1.RealApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TeamController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet("getAllTeam")]
        public IEnumerable<Team> Get()
        {
            try
            {
                return _db.Teams;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("getTeamById/{id:int}")]
        public Team GetTeamById(int id)
        {
            try
            {
                return _db.Teams.Find(id);
            }
            catch
            {
                return null;
            }
        }


        [HttpPost("createTeam")]
        public IActionResult Post([FromForm] Team team, IFormFile imagePath)
        {
            try
            {
                if(imagePath!=null)
                {
                    var memory = new MemoryStream();
                    imagePath.CopyTo(memory);

                    team.ImagePath = memory.ToArray();
                }
                
                team.CreateAt = DateTime.Now;
                team.CreatedBy = "admin";

                _db.Teams.Add(team);
                _db.SaveChanges();

                return Ok(new { message = "Create successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }          
        }
    }
}
