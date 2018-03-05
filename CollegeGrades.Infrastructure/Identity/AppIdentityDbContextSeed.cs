using CollegeGrades.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CollegeGrades.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            var defaultUser = new User { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
            await userManager.CreateAsync(defaultUser, "Pass@word1");
        }
    }
}