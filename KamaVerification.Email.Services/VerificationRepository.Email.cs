using KamaVerification.Email.Data;
using KamaVerification.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace KamaVerification.Email.Services
{
    public interface IEmailVerificationRepository
    {
        Task SendAsync();
    }

    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly ILogger<EmailVerificationRepository> _logger;
        private readonly IConfiguration _config;
        private readonly IVerificationRepository _verificationRepo;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly ISendGridClient _sendGridClient;

        public EmailVerificationRepository(
            ILogger<EmailVerificationRepository> logger,
            IConfiguration config,
            IVerificationRepository verificationRepo,
            IEmailTemplateRepository emailTemplateRepository,
            ISendGridClient sendGridClient)
        {
            _logger = logger;
            _config = config;
            _verificationRepo = verificationRepo;
            _emailTemplateRepository = emailTemplateRepository;
            _sendGridClient = sendGridClient;

        }

        public async Task SendAsync()
        {
            // Generate code
            var code = _verificationRepo.GenerateCode();

            var msg = new SendGridMessage
            {
                From = new EmailAddress(_config[Keys.EmailFrom], "KamaVerification"),
                Subject = "Sending with SendGrid is Fun"
            };
            msg.AddContent(MimeType.Html, _emailTemplateRepository.Get(code));
            msg.AddTo(new EmailAddress(_config[Keys.EmailFrom], "Example User"));

            var response = await _sendGridClient.SendEmailAsync(msg);

            if (response.IsSuccessStatusCode) _logger.LogInformation("Successfully sent email");
            else throw new Exception(string.Join(", ", response.Body));
        }
    }
}