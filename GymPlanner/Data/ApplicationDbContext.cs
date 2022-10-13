using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GymPlanner.Models;

namespace GymPlanner.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GymPlanner.Models.Day> Day { get; set; }
        public DbSet<GymPlanner.Models.Workout> Workout { get; set; }
    }
}