using System.Threading.Tasks;
using CollegeGrades.Core.Entities;
using CollegeGrades.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CollegeGrades.Infrastructure.Services
{
    public class AccountManager 
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountManager(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void ConfirmEmail(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task EditAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task ForgotPasswordAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public bool IsResetPasswordKeyValid(string key)
        {
            throw new System.NotImplementedException();
        }

        public User LogIn(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task RegisterAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public void ResetPassword(string key, string newPassword)
        {
            throw new System.NotImplementedException();
        }
    }
}