using CollegeGrades.Core.Interfaces;
using CollegeGrades.Infrastructure.Data;
using System.Threading.Tasks;

namespace CollegeGrades.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUserService Users { get; set; }
        public ITeacherRepository Teachers { get; set; }

        public UnitOfWork(
            ApplicationDbContext context,
            IUserService userService,
            ITeacherRepository teacherRepository)
        {
            _context = context;
            Users = userService;
            Teachers = teacherRepository;
        }

        public async Task CompletedAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}