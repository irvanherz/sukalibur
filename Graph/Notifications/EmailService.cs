using Amazon.SimpleEmailV2;
using Microsoft.Extensions.Options;
using Sukalibur.Graph.Users;
using Sukalibur.Shared.Options;
using System.Text.Json;

namespace Sukalibur.Graph.Notifications
{
    public class EmailService
    {
        private readonly CommonOptions _commonOptions;
        private readonly AmazonSimpleEmailServiceV2Client _sesClient;
        public EmailService(IOptions<CommonOptions> commonOptions, AmazonSimpleEmailServiceV2Client sesClient)
        {
            _commonOptions = commonOptions.Value;
            _sesClient = sesClient;
        }

        public async Task<bool> SendWelcomeMessageAsync(User user)
        {
            var templateData = JsonSerializer.Serialize(new
            {
                name = user.FullName,
                action_url = $"{_commonOptions.WebAppBaseUrl}/auth/login"
            });
            var sendEmailRequest = new Amazon.SimpleEmailV2.Model.SendEmailRequest
            {
                FromEmailAddress = _commonOptions.NoreplyEmailAddress,
                Destination = new Amazon.SimpleEmailV2.Model.Destination
                {
                    ToAddresses = [user.Email]
                },
                Content = new Amazon.SimpleEmailV2.Model.EmailContent
                {
                    Template = new Amazon.SimpleEmailV2.Model.Template
                    {
                        TemplateName = "WelcomeMessage",
                        TemplateData = templateData,
                    }
                }
            };
            var sendEmailResponse = await _sesClient.SendEmailAsync(sendEmailRequest);
            return sendEmailResponse.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
