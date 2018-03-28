using CollegeGrades.Core.Entities;
using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>, IRepositoryAsync<Teacher>
    {
        
    }
}