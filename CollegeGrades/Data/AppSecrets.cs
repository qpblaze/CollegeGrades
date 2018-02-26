namespace CollegeGrades.Data
{
    /// <summary>
    /// SendGrid api secrets
    /// https://sendgrid.com/
    /// </summary>
    public class SendGridSecrets
    {
        public string ApiKey { get; set; }
    }

    public class AppSecrets
    {
        public SendGridSecrets SendGrid { get; set; }
    }
}