using CollegeGrades.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeGrades.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Cycle> Cycles { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<AttendedSubject> AttendedSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().ToTable(nameof(Subjects));
            modelBuilder.Entity<Teacher>().ToTable(nameof(Teachers));
            modelBuilder.Entity<Cycle>().ToTable(nameof(Cycles));
            modelBuilder.Entity<Score>().ToTable(nameof(Scores));
            modelBuilder.Entity<AttendedSubject>().ToTable(nameof(AttendedSubjects));
        }
    }
}