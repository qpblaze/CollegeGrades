using CollegeGrades.Core.Entities;
using CollegeGrades.Core.Interfaces;
using CollegeGrades.Models.AccountViewModels;
using CollegeGrades.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CollegeGrades.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Privete Properties

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        #endregion Privete Properties

        #region Constructor

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        #endregion Constructor

        #region Private Methods

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        #endregion Private Methods

        #region Register

        [HttpGet]
        [AllowAnonymous]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                AddErrors(result);
                return View(model);
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);

            await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);

            return RedirectToAction(nameof(ConfirmationEmailSent));
        }

        #endregion Register

        #region LogIn

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult LogIn(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(HomeController.Index), "Home");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> LogIn(LogInViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (!user.EmailConfirmed)
                {
                    ModelState.AddModelError("Email", "The email is not cofirmed.");
                    return View(model);
                }

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ModelState.AddModelError("Email", "Invalid email and/or password.");
            return View(model);
        }

        #endregion LogIn

        #region LogOut

        [HttpPost]
        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(LogIn));
        }

        #endregion LogOut

        #region Email Confirmation

        [HttpGet]
        [AllowAnonymous]
        [Route("confirmation-email-sent")]
        public IActionResult ConfirmationEmailSent()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
                return RedirectToAction(nameof(HomeController.Index), "Home");

            return View();
        }

        #endregion Email Confirmation

        #region Reset Password

        [HttpGet]
        [AllowAnonymous]
        [Route("reset-password")]
        public IActionResult ResetPassword()
        {
            return View();
        }

        #endregion Reset Password

        #region Profile

        [HttpGet]
        [AllowAnonymous]
        [Route("profile")]
        public async Task<IActionResult> ViewProfile(string id = null)
        {
            User user = null;
            if(id == null)
            {
                user = await _userManager.GetUserAsync(HttpContext.User);
            }
            else
            {
                user = await _userManager.FindByIdAsync(id);
            }



            return View();
        } 

        #endregion
    }
}