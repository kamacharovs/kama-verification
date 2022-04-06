using KamaVerification.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KamaVerification.Core.Controllers
{
    [ApiController]
    [Route("v1/verification")]
    public class EmailController : ControllerBase
    {
        private readonly IVerificationRepository _repo;

        public EmailController(IVerificationRepository repo)
        {
            _repo = repo;
        }

        [Authorize]
        [HttpGet]
        [Route("code")]
        public IActionResult GetCode()
        {
            return Ok(new { code = _repo.GenerateCode() });
        }
    }
}