using System.Security.Claims;

namespace CollegeGrades.Web.Extensions
{
    public static class UserExtension
    {
        public static string GetRole(this ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin"))
                return "Admin";
            if (user.IsInRole("Student"))
                return "Student";
            if (user.IsInRole("Manager"))
                return "Manager";

            return "Unknown";
        }
    }
}