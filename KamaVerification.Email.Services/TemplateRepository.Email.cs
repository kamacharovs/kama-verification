using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace KamaVerification.Email.Services
{
    public interface IEmailTemplateRepository
    {
        string Get();
    }

    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly ILogger<EmailTemplateRepository> _logger;
        private readonly IConfiguration _config;

        public EmailTemplateRepository(
            ILogger<EmailTemplateRepository> logger,
            IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public string Get()
        {
            return @"
                <!DOCTYPE html>
                <html>
                <head>
                <style>
                .custom-text {
                    display: block;
                    margin-block-start: 1em;
                    margin-block-end: 1em;
                    margin-inline-start: 0px;
                    margin-inline-end: 0px;
                }

                </style>
                </head>
                <body>

                <div class=""custom-text"">
                    <p>Please enter the following code:</p>
                </div>

                <br>
                    <strong>{{code}}</strong>
                <br><br>

                <div class=""custom-text"">
                    <p>This code will be active for {{expiresIn}} minutes. If it has expired you can request a new code from the verification page.</p>
                </div>
                <br>

                </body>
                </html>";
        }
    }
}
