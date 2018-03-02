using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CollegeGrades.Web.Extensions
{
    public static class HtmlHelperExtension
    {
        public static string ControllerName(this IHtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
        }

        public static string ActionName(this IHtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["action"].ToString();
        }

        public static bool ShouldDisplayNav(this IHtmlHelper htmlHelper)
        {
            string action = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
            List<string> actionsWithNoNav = new List<string>
            {
                nameof(Controllers.AccountController.ConfirmationEmailSent),
                nameof(Controllers.AccountController.ConfirmEmail),
                nameof(Controllers.AccountController.LogIn),
                nameof(Controllers.AccountController.Register)
            };

            foreach(var item in actionsWithNoNav)
            {
                if (item == action)
                    return false;
            }
            
            return true;
        }
    }
}