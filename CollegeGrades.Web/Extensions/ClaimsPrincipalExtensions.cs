using System;
using System.Security.Claims;

namespace CollegeGrades.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetRole(this ClaimsPrincipal principal)
        {
            if (principal.IsInRole("Admin"))
                return "Admin";
            if (principal.IsInRole("Student"))
                return "Student";
            if (principal.IsInRole("Manager"))
                return "Manager";

            return "Unknown";
        }
    }
}