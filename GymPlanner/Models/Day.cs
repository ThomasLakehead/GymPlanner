using System.ComponentModel.DataAnnotations;

namespace GymPlanner.Models
{
    public class Day
    {
        public int DayId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<Day>? Days { get; set; }
    }
}
