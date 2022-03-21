using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace KamaVerification.Email.Services
{
    public interface IEmailTemplateRepository
    {
        string Get(string code);
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

        public string Get(string code)
        {
            return $"<strong>Please enter the following code:<br><br><strong>{code}</strong><br><br></strong>";
        }
    }
}
