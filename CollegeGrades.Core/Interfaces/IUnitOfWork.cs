using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserService Users { get; }
        ITeacherRepository Teachers { get; }

        Task CompletedAsync();
    }
}