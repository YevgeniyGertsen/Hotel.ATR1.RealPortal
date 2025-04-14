using Hotel.ATR1.RealApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.ATR1.RealApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PositionController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Position> Get()
        {
           return _db.Positions;
        }

        [HttpPost("createPosition")]
        public IActionResult Post(Position position)
        {
            try
            {
                _db.Positions.Add(position);
                _db.SaveChanges();
                return Ok(new { message = "Create successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("createPosition2")]
        public IActionResult createPosition2([FromForm]Position position)
        {
            try
            {
                _db.Positions.Add(position);
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