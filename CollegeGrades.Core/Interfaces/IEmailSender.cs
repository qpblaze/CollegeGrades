using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}