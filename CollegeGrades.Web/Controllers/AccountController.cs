using AutoMapper;
using CollegeGrades.Core.Entities;
using CollegeGrades.Core.Exceptions;
using CollegeGrades.Core.Interfaces;
using CollegeGrades.Web.Attributes;
using CollegeGrades.Web.Extensions;
using CollegeGrades.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CollegeGrades.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Private Properties

        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        #endregion Private Properties

        #region Constructor

        public AccountController(
            IUnitOfWork unitOfWork,
            IEmailSender emailSender,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        #endregion Constructor

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
            User user = _mapper.Map<RegisterViewModel, User>(model);

            try
            {
                await _unitOfWork.Users.CreateAsync(user);
            }
            catch (InvalidInputException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                return View(model);
            }

            try
            {
                await SendConfirmationEmail(user);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Email", "An error occured while sending the confirmation email.");
                return View(model);
            }

            return RedirectToAction(nameof(ConfirmationEmailSent));
        }

        private async Task SendConfirmationEmail(User user)
        {
            var code = await _unitOfWork.Users.GenerateEmailConfirmationTokenAsync(user);
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
            try
            {
                await _unitOfWork.Users.SignInAsync(model.Email, model.Password);
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
            await _unitOfWork.Users.SignOutAsync();

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
                await _unitOfWork.Users.ConfirmEmailAsync(userID, code);
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

            var user = await _unitOfWork.Users.FindByIdAsync(id);

            if (user == null)
                return RedirectToAction(nameof(HomeController.Index), "Home");

            return base.View(_mapper.Map<User, Web.Models.User.ProfileViewModel>(user));
        }

        #endregion Profile
    }
}