using Microsoft.EntityFrameworkCore;

namespace Hotel.ATR1.Admin.Models
{
    public class AddDbContext : DbContext
    {
        public AddDbContext(DbContextOptions<AddDbContext> options)
            :base(options) { }

        public DbSet<Position> Positions { get; set; }
    }
}