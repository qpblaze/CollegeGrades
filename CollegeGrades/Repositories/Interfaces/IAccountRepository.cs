using CollegeGrades.Models;
using System.Threading.Tasks;

namespace CollegeGrades.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task RegisterAccountAsync(Account account);

        Account LogIn(Account account);

        Task ForgotPasswordAsync(Account account);

        void ConfirmEmail(string key);

        bool IsResetPasswordKeyValid(string key);

        void ResetPassword(string key, string newPassword);

        Task EditAsync(Account account);

        void Delete(Account account);
    }
}