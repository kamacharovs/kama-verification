using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KamaVerification.Services;
using KamaVerification.Data.Dtos;
using KamaVerification.Email.Services;
using KamaVerification.Email.Data.Dtos;

namespace KamaVerification.Email.Core.Controllers
{
    [ApiController]
    [Route("v1/email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailVerificationRepository _repo;
        private readonly ICustomerRepository _customerRepo;

        public EmailController(IEmailVerificationRepository repo, ICustomerRepository customerRepo)
        {
            _repo = repo;
            _customerRepo = customerRepo;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequest request)
        {
            return Ok(await _customerRepo.GetTokenAsync(request));
        }

        [Authorize]
        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendAsync([FromBody, Required] EmailRequest request)
        {
            return Ok(new { code = await _repo.SendAsync(request) });
        }
    }
}