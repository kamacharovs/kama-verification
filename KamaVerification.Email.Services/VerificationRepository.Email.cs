using KamaVerification.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;
using KamaVerification.Data.Models;

namespace KamaVerification.Email.Services
{
    public interface IEmailVerificationRepository
    {
        Task SendAsync(Customer customer);
    }

    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly ILogger<EmailVerificationRepository> _logger;
        private readonly IVerificationRepository _verificationRepo;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly ISendGridClient _sendGridClient;

        public EmailVerificationRepository(
            ILogger<EmailVerificationRepository> logger,
            IVerificationRepository verificationRepo,
            IEmailTemplateRepository emailTemplateRepository,
            ISendGridClient sendGridClient)
        {
            _logger = logger;
            _verificationRepo = verificationRepo;
            _emailTemplateRepository = emailTemplateRepository;
            _sendGridClient = sendGridClient;

        }

        public async Task SendAsync(Customer customer)
        {
            var config = customer.EmailConfig;
            var code = _verificationRepo.GenerateCode();

            var msg = new SendGridMessage
            {
                From = new EmailAddress(config.FromEmail, config.FromName),
                Subject = config.Subject
            };
            msg.AddContent(MimeType.Html, _emailTemplateRepository.Get().Replace("{{code}}", code));
            msg.AddTo(new EmailAddress(config.FromEmail, "Example User"));

            var response = await _sendGridClient.SendEmailAsync(msg);

            if (response.IsSuccessStatusCode) _logger.LogInformation("Successfully sent email for Customer={CustomerName}", customer.Name);
            else throw new Exception(string.Join(", ", response.Body));
        }
    }
}