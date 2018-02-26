using CollegeGrades.Data;
using CollegeGrades.Enums;
using CollegeGrades.Models;
using CollegeGrades.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeGrades.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public ApplicationDbContext CollegeContext;

        private readonly IEmailSenderRepository _emailSenderRepository;
        private readonly IPasswordManager _passwordManager;

        public AccountRepository(ApplicationDbContext context, IOptions<AppSecrets> options)
            : base(context)
        {
            CollegeContext = context;
            _emailSenderRepository = new MessageSenderRepository(options);
            _passwordManager = new PasswordManager();
        }

        public async Task RegisterAccountAsync(Account account)
        {
            var acc = Find(x => x.Email.ToLower() == account.Email.ToLower()).FirstOrDefault();

            if (acc != null)
                throw new Exception("This email already exists.");

            account.ID = Guid.NewGuid().ToString();
            account.ConfirmEmailToken = Guid.NewGuid().ToString();
            account.RoleType = RoleType.Student;
            account.Password = _passwordManager.HashPassword(account.Password);

            account.ProfileImage = $"www.gravatar.com/avatar/{account.ID.Replace('-', 'f')}?s=300&d=identicon";

            await AddAsync(account);

            await _emailSenderRepository.SendEmailAsync(account.Email, "College Grades - Confirm Email", "/confirm-email/" + account.ConfirmEmailToken);
        }

        public Account LogIn(Account account)
        {
            var acc = Find(x => x.Email.ToLower() == account.Email.ToLower() && _passwordManager.ValidatePassword(account.Password, x.Password)).FirstOrDefault();
            if (acc == null)
                throw new Exception("Invalid email and/or password.");

            if (acc.IsVerified == false)
                throw new Exception("This account is not verified.");

            return acc;
        }

        public async Task ForgotPasswordAsync(Account account)
        {
            account = Find(x => x.Email.ToLower() == account.Email.ToLower()).FirstOrDefault();
            if (account == null)
                throw new Exception("This account does not exists.");

            account.ResetPasswordToken = Guid.NewGuid().ToString();

            await _emailSenderRepository.SendEmailAsync(account.Email, "College Grades - Forgot password", "/reset-password/" + account.ResetPasswordToken);
        }

        public void ConfirmEmail(string key)
        {
            var account = Find(x => x.ConfirmEmailToken == key).FirstOrDefault();
            if (account == null)
                throw new Exception("An account with this confirmation key does not exists.");

            account.ConfirmEmailToken = null;
            account.IsVerified = true;
        }

        public bool IsResetPasswordKeyValid(string key)
        {
            var account = Find(x => x.ResetPasswordToken == key).FirstOrDefault();
            if (account == null)
                return false;

            return true;
        }

        public void ResetPassword(string key, string newPassword)
        {
            var account = Find(x => x.ResetPasswordToken == key).FirstOrDefault();
            if (account != null)
            {
                account.Password = _passwordManager.HashPassword(newPassword);
                account.ResetPasswordToken = null;
            }
        }

        public async Task EditAsync(Account account)
        {
            if (string.IsNullOrEmpty(account.RoleTypeID.ToString()))
                throw new Exception("No role selected.");

            var oldAccount = await GetAsync(account.ID);

            oldAccount.RoleTypeID = account.RoleTypeID;
        }

        public void Delete(Account account)
        {
            Remove(account);
        }
    }
}