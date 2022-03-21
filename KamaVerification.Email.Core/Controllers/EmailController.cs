using KamaVerification.Email.Services;
using KamaVerification.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace KamaVerification.Email.Core.Controllers
{
    [ApiController]
    [Route("v1/email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailVerificationRepository _repo;

        public EmailController(IEmailVerificationRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("send")]
        public async Task<IActionResult> SendAsync()
        {
            await _repo.SendAsync(new Customer
            {
                CustomerId = 1,
                PublicKey = Guid.NewGuid(),
                Name = "Test",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                EmailConfig = new CustomerEmailConfig
                {
                    CustomerEmailConfigId = 1,
                    PublicKey = Guid.NewGuid(),
                    CustomerId = 1,
                    Subject = "Your customer verification code",
                    FromEmail = "yifet83692@sofrge.com",
                    FromName = "Test Customer",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt= DateTime.UtcNow
                }
            });

            return Ok();
        }
    }
}