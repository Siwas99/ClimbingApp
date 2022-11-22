using System.ComponentModel.DataAnnotations;

namespace ClimbingApp.Models
{
    public class Rock
    {
        public int RockId { get; set; }

        public string Name { get; set; }

        public Region Region { get; set; }

    }
}
