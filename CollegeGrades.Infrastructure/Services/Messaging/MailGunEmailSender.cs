using CollegeGrades.Core;
using CollegeGrades.Core.Interfaces;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

namespace CollegeGrades.Infrastructure.Services.Messaging
{
    public class MailGunEmailSender : IEmailSender
    {
        private readonly string API_KEY;
        private readonly string EMAIL;
        private readonly string NAME;

        public MailGunEmailSender(IOptions<AppSecrets> options)
        {
            API_KEY = options.Value.SendGrid.ApiKey;
            EMAIL = options.Value.SendGrid.Email;
            NAME = options.Value.SendGrid.Name;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", API_KEY);

            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox89776fd384bf4d49a14f05162f672bf4.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <mailgun@sandbox89776fd384bf4d49a14f05162f672bf4.mailgun.org>");
            request.AddParameter("to", email);
            request.AddParameter("subject", subject);
            request.AddParameter("text", message);
            request.Method = Method.POST;

            IRestResponse respone = client.Execute(request);

            return Task.CompletedTask;
        }
    }
}
