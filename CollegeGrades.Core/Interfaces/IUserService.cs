using CollegeGrades.Core.Entities;
using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Finds and returns a user, if any, who has the specified id.
        /// </summary>
        /// <param name="id">The user ID to search for.</param>
        /// <returns></returns>
        Task<User> FindByIdAsync(string id);

        /// <summary>
        /// Add the specified user to the named role.
        /// </summary>
        /// <param name="user">The user to add to the named role.</param>
        /// <param name="name">The name of the role to add the user to.</param>
        /// <returns></returns>
        Task AddToRoleAsync(User user, string name);

        /// <summary>
        /// Creates the specific user in the backing store.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns></returns>
        Task CreateAsync(User user);

        /// <summary>
        /// Attempts to sign in the specific email and password.
        /// </summary>
        /// <param name="email">The email to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <returns></returns>
        Task SignInAsync(string email, string password);

        /// <summary>
        /// Signs the current user out of the application.
        /// </summary>
        /// <returns></returns>
        Task SignOutAsync();

        /// <summary>
        /// Generates an email confirmation token for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// An email confirmation token as a string.
        /// </returns>
        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        /// <summary>
        /// Validates that an email confirmation token matches the specified user.
        /// </summary>
        /// <param name="userID">The userID to validate the token against.</param>
        /// <param name="token">The email confirmation token to validate.</param>
        /// <returns></returns>
        Task ConfirmEmailAsync(string userID, string token);
    }
}