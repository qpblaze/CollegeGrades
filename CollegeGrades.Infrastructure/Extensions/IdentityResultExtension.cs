using CollegeGrades.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace CollegeGrades.Infrastructure.Extensions
{
    internal static class IdentityResultExtension
    {
        internal static IResultStatus AsIResultStatus(this IdentityResult result)
        {
            return ResultStatus.Create(result.Errors.Select(x => x.Description));
        }

        internal static IResultStatus AsIResultStatus(this SignInResult result)
        {
            return ResultStatus.Create();
        }
    }
}