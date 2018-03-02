using CollegeGrades.Core.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CollegeGrades.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _apiKey = "";

        private const string EMAIL = "college.grades@ursaciuc.me";
        private const string NAME = "College Grades";

        //public MessageSenderService(IOptions<AppSecrets> options)
        //{
        //    _apiKey = options.Value.SendGrid.ApiKey;
        //}

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(EMAIL, NAME);
            var to = new EmailAddress(email, "");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);
            var response = await client.SendEmailAsync(msg);
        }
    }
}