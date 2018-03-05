namespace CollegeGrades.Core
{
    /// <summary>
    /// SendGrid api secrets
    /// https://sendgrid.com/
    /// </summary>
    public class SendGrid
    {
        public string ApiKey { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class AppSecrets
    {
        public SendGrid SendGrid { get; set; }
    }
}