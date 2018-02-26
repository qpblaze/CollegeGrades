using CollegeGrades.Data;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CollegeGrades.Repositories
{
    public class MessageSenderRepository : IEmailSenderRepository
    {
        private readonly string _apiKey;

        private const string EMAIL = "college.grades@ursaciuc.me";
        private const string NAME = "College Grades";

        public MessageSenderRepository(IOptions<AppSecrets> options)
        {
            _apiKey = options.Value.SendGrid.ApiKey;
        }

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