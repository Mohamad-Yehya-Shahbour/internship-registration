using internship_registration.Models;
using Microsoft.EntityFrameworkCore;

namespace internship_registration.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Program> Programs { get; set; } = null!;
        public DbSet<AuthUser> AuthUsers { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Applicant> Applicants { get; set; } = null!;
        public DbSet<ProgramUser> ProgramUsers { get; set; } = null!;




    }
}
