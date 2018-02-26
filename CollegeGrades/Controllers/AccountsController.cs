using AutoMapper;
using CollegeGrades.Data;
using CollegeGrades.Models;
using CollegeGrades.Models.AccountViewModels;
using CollegeGrades.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollegeGrades.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public AccountsController(IMapper mapper, ApplicationDbContext context, IOptions<AppSecrets> options)
        {
            _mapper = mapper;
            _unitOfWork = new UnitOfWork(context, options);
        }

        [Route("login")]
        public IActionResult LogIn()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogInAsync(LogInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Account account = _mapper.Map<LogInViewModel, Account>(model);

            try
            {
                account = _unitOfWork.Accounts.LogIn(account);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(model);
            }

            // Set the claims we need and add login the user
            var claims = new List<Claim>
            {
                new Claim("FullName", account.FirstName + " " + account.LastName),
                new Claim("ID", account.ID),
                new Claim("Email", account.Email),
                new Claim("ProfileImage", account.ProfileImage),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Account account = _mapper.Map<RegisterViewModel, Account>(model);
            try
            {
                await _unitOfWork.Accounts.RegisterAccountAsync(account);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(model);
            }

            await _unitOfWork.CompletedAsync();

            return RedirectToAction(nameof(LogIn));
        }
    }
}