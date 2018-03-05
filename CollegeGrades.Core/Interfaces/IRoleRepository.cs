using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Creates the specified role in the persistence store.
        /// </summary>
        /// <param name="name">The name of the new role.</param>
        /// <returns></returns>
        Task CreateAsync(string name);

        /// <summary>
        /// Gets a flag indicating whether the specified roleName exists.
        /// </summary>
        /// <param name="name">The role name whose existence should be checked.</param>
        /// <returns>True if the role name exists, otherwise false.</returns>
        Task<bool> RoleExistsAsync(string name);
    }
}