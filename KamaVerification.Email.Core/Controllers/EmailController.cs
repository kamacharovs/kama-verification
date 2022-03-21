using KamaVerification.Email.Services;
using KamaVerification.Email.Data.Dtos;
using System.ComponentModel.DataAnnotations;
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

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendAsync([FromBody, Required] EmailRequest request)
        {
            return Ok(new { code = await _repo.SendAsync(request) });
        }
    }
}