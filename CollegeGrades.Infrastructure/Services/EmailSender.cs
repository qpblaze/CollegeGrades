using CollegeGrades.Core;
using CollegeGrades.Core.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CollegeGrades.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string API_KEY;
        private readonly string EMAIL;
        private readonly string NAME;

        public EmailSender(IOptions<AppSecrets> options)
        {
            API_KEY = options.Value.SendGrid.ApiKey;
            EMAIL = options.Value.SendGrid.Email;
            NAME = options.Value.SendGrid.Name;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress(EMAIL, NAME);
            var to = new EmailAddress(email, "");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);
            var response = await client.SendEmailAsync(msg);
        }
    }
}