using CollegeGrades.Core.Entities;
using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(User user, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task SignInAsync(string email, string password);

        Task<User> FindByIdAsync(string id);
        Task ConfirmEmailAsync(string userID, string code);
        Task SignOutAsync();
    }
}