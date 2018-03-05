using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email to a specific email address.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}