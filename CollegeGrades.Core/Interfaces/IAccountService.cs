using CollegeGrades.Core.Entities;
using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface IAccountService : IRepository<Account>
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