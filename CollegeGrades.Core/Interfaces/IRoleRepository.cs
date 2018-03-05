using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task CreateAsync(string name);

        Task<bool> RoleExistsAsync(string name);
    }
}