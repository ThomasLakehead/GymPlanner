using System.ComponentModel.DataAnnotations;

namespace GymPlanner.Models
{
    public class Workout
    {
        public int WorkoutId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public int? Reps { get; set; }

        [Required]
        public int? Sets { get; set; }

        [Required]
        public string? Weight { get; set; }

        [Display(Name = "Day")]
        public int DayId { get; set; }

        public Day? Day { get; set; }

    }
}
