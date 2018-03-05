using CollegeGrades.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CollegeGrades.Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateAsync(string name)
        {
            var role = new IdentityRole
            {
                Name = name
            };

            await _roleManager.CreateAsync(role);
        }

        public async Task<bool> RoleExistsAsync(string name)
        {
            bool exists = await _roleManager.RoleExistsAsync(name);

            return exists;
        }
    }
}