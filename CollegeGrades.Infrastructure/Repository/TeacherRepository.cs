using CollegeGrades.Core.Entities;
using CollegeGrades.Core.Interfaces;
using CollegeGrades.Infrastructure.Data;

namespace CollegeGrades.Infrastructure.Repository
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}