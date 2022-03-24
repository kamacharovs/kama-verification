using KamaVerification.Services;
using KamaVerification.Data.Models;
using KamaVerification.Email.Data;
using KamaVerification.Email.Data.Dtos;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace KamaVerification.Email.Services
{
    public interface IEmailVerificationRepository
    {
        Task<string> SendAsync(EmailRequest request);
    }

    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly ILogger<EmailVerificationRepository> _logger;
        private readonly IConfiguration _config;
        private readonly IVerificationRepository _verificationRepo;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly ISendGridClient _sendGridClient;

        public EmailVerificationRepository(
            ILogger<EmailVerificationRepository> logger,
            IConfiguration config,
            IVerificationRepository verificationRepo,
            ICustomerRepository customerRepository,
            IEmailTemplateRepository emailTemplateRepository,
            ISendGridClient sendGridClient)
        {
            _logger = logger;
            _config = config;
            _verificationRepo = verificationRepo;
            _customerRepository = customerRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _sendGridClient = sendGridClient;
        }

        public async Task<string> SendAsync(EmailRequest request)
        {
            var customer = await _customerRepository.GetAsync(1);
            var customerEmailConfig = customer.EmailConfig;
            var code = _verificationRepo.GenerateCode();
            var template = _emailTemplateRepository.GetDefault()
                .Replace("{{code}}", code)
                .Replace("{{expiresIn}}", customerEmailConfig.ExpirationInMinutes.ToString());

            var msg = new SendGridMessage
            {
                From = new EmailAddress(_config[Keys.EmailFrom], customerEmailConfig.FromName),
                Subject = customerEmailConfig.Subject
            };
            msg.AddContent(MimeType.Html, template);
            msg.AddTo(new EmailAddress(request.To, request.Name));

            var response = await _sendGridClient.SendEmailAsync(msg);

            if (response.IsSuccessStatusCode) _logger.LogInformation("Successfully sent email for Customer={CustomerName}", customer.Name);
            else throw new Exception(string.Join(", ", response.Body));

            return code;
        }
    }
}