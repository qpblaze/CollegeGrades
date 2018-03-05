using AutoMapper;
using CollegeGrades.Core.Entities;
using CollegeGrades.Core.Exceptions;
using CollegeGrades.Core.Interfaces;
using CollegeGrades.Infrastructure.Repository;
using CollegeGrades.Infrastructure.Services;
using CollegeGrades.Models.AccountViewModels;
using CollegeGrades.Web.Attributes;
using CollegeGrades.Web.Extensions;
using CollegeGrades.Web.Models.User;
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

        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        #endregion Privete Properties

        #region Constructor

        public AccountController(
            IUserService userService,
            IEmailSender emailSender,
            IMapper mapper)
        {
            _userService = userService;
            _emailSender = emailSender;
            _mapper = mapper;
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
        [RedirectLoggedUser]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [RedirectLoggedUser]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User user = _mapper.Map<RegisterViewModel, User>(model);

            try
            {
                await _userService.RegisterAsync(user, model.Password);
            }
            catch (InvalidInputException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                return View(model);
            }

            await SendConfirmationEmail(user);

            return RedirectToAction(nameof(ConfirmationEmailSent));
        }

        private async Task SendConfirmationEmail(User user)
        {
            var code = await _userService.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);

            await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);
        }

        #endregion Register

        #region LogIn

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        [RedirectLoggedUser]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [RedirectLoggedUser]
        public async Task<IActionResult> LogIn(LogInViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _userService.SignInAsync(model.Email, model.Password);
            }
            catch (InvalidInputException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion LogIn

        #region LogOut

        [HttpPost]
        public async Task<ActionResult> LogOut()
        {
            await _userService.SignOutAsync();

            return RedirectToAction(nameof(LogIn));
        }

        #endregion LogOut

        #region Email Confirmation

        [HttpGet]
        [AllowAnonymous]
        [RedirectLoggedUser]
        [Route("confirmation-email-sent")]
        public IActionResult ConfirmationEmailSent()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [RedirectLoggedUser]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userID, string code)
        {
            try
            {
                await _userService.ConfirmEmailAsync(userID, code);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View();
        }

        #endregion Email Confirmation

        #region Reset Password

        [HttpGet]
        [AllowAnonymous]
        [RedirectLoggedUser]
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
            if (id == null)
                id = User.GetUserId();

            var user = await _userService.FindByIdAsync(id);

            if (user == null)
                return RedirectToAction(nameof(HomeController.Index), "Home");

            return View(_mapper.Map<User, DisplayUserViewModel>(user));
        }

        #endregion Profile
    }
}