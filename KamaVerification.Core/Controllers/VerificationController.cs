using KamaVerification.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        [Authorize]
        [HttpGet]
        [Route("code/compare/{givencode}/{expectedcode}")]
        public IActionResult CalculateDifference(
            [FromRoute, Required] string givenCode,
            [FromRoute, Required] string expectedCode)
        {
            return Ok(new { code = _repo.CalculateDifference(givenCode, expectedCode) });
        }
    }
}