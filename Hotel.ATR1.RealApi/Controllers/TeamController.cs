using Hotel.ATR1.RealApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.ATR1.RealApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TeamController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("getAllTeam")]
        public async Task<List<Team>> Get()
        {
            try
            {
                return await _db.Teams.Include(p => p.Position).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("getTeamById/{id:int}")]
        public async Task<Team?> GetTeamById(int id)
        {
            try
            {
                return await _db.Teams
                                .Include(p => p.Position)
                                .FirstOrDefaultAsync(f => f.Id == id);
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
                return BadRequest(new { message = ex.Message });
            }          
        }
    }
}