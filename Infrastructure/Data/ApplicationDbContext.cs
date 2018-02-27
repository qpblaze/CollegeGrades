using CollegeGrades.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeGrades.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Cycle> Cycles { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<AttendedSubject> AttendedSubjects { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Account>().ToTable(nameof(Accounts));
        //    modelBuilder.Entity<Area>().ToTable(nameof(Areas));
        //    modelBuilder.Entity<Sponsor>().ToTable(nameof(Sponsors));
        //}
    }
}