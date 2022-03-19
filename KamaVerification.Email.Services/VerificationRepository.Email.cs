using Microsoft.Extensions.Logging;
using KamaVerification.Services;
using FluentEmail.Core;

namespace KamaVerification.Email.Services
{
    public interface IEmailVerificationRepository
    {
        Task SendAsync();
    }

    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly ILogger<EmailVerificationRepository> _logger;
        private readonly IVerificationRepository _repo;
        private readonly IFluentEmail _fluentEmail;

        public EmailVerificationRepository(
            ILogger<EmailVerificationRepository> logger,
            IVerificationRepository repo, 
            IFluentEmail fluentEmail)
        {
            _logger = logger;
            _repo = repo;
            _fluentEmail = fluentEmail;

        }

        public async Task SendAsync()
        {
            var email = _fluentEmail
                .SetFrom("bill.gates@microsoft.com")
                .To("gkamacharov@gmail.com", "Georgi")
                .Subject("Hi Georgi!")
                .Body("Fluent email looks great!");
            var emailResponse = await email.SendAsync();

            if (emailResponse.Successful) _logger.LogInformation("Successfully sent email");
        }
    }
}