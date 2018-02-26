using System.Threading.Tasks;

namespace CollegeGrades.Repositories
{
    public interface IEmailSenderRepository
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}