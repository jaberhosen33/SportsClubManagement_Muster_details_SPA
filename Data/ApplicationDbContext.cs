using Microsoft.EntityFrameworkCore;
using SportsClubManagement_SPA_Muster_details.Models;

namespace SportsClubManagement_SPA_Muster_details.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API Configurations if needed
            base.OnModelCreating(modelBuilder);
        }
    }
}
