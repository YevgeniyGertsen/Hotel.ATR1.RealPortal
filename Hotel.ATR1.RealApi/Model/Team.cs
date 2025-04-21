using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hotel.ATR1.RealApi.Model
{
    [Table("atr3Team")]
    public class Team
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "admin";

        public string? FirstName { get; set; } = null;
        public string? SecondName { get; set; } = null;
        public string? MiddleName { get; set; } = null;
        public string? AboutTeam { get; set; } = null;
        public byte[]? ImagePath { get; set; } = null;
        public int PositionId { get; set; }
        public Position? Position { get; set; } = null;
    }
}
