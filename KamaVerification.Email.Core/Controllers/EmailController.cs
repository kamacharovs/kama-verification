using KamaVerification.Email.Services;
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
            await _repo.SendAsync();

            return Ok();
        }
    }
}