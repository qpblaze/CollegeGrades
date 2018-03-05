using CollegeGrades.Core.Entities;
using CollegeGrades.Core.Exceptions;
using CollegeGrades.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CollegeGrades.Infrastructure.Services
{
    public class UserService : IUserService
    {
        #region Private Properties

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRoleRepository _roleRepository;

        #endregion Private Properties

        #region Constructor

        public UserService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IRoleRepository roleRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleRepository = roleRepository;
        }

        #endregion Constructor

        #region Private Methods

        private string GetProfileImageURL()
        {
            return "http://www.gravatar.com/avatar/" + Guid.NewGuid().ToString().Replace('-', '0') + "?&default=identicon&forcedefault=1&s=300";
        }

        private async Task AddToRoleAsync(User user, string name)
        {
            bool exists = await _roleRepository.RoleExistsAsync(name);
            if (!exists)
                await _roleRepository.CreateAsync(name);

            await _userManager.AddToRoleAsync(user, name);
        }

        #endregion Private Methods

        public async Task RegisterAsync(User user, string password)
        {
            // Default properties
            user.ProfileImage = GetProfileImageURL();

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new InvalidInputException(result.Errors.ToString());
            }

            await AddToRoleAsync(user, "Student");
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task SignInAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (!result.Succeeded)
                throw new InvalidInputException("Email", "Invalid email and/or password.");

            var user = await _userManager.FindByEmailAsync(email);
            if (!user.EmailConfirmed)
            {
                throw new InvalidInputException("Email", "The email is not cofirmed.");
            }
        }

        public async Task<User> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task ConfirmEmailAsync(string userID, string code)
        {
            if (userID == null)
                throw new ArgumentNullException(nameof(userID));
            if (code == null)
                throw new ArgumentNullException(nameof(code));

            var user = await FindByIdAsync(userID);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userID}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}