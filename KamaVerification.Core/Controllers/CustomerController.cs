using KamaVerification.Data.Dtos;
using KamaVerification.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace KamaVerification.Core.Controllers
{
    [ApiController]
    [Route("v1/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repo;

        public CustomerController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetByNameAsync([FromRoute, Required] string name)
        {
            return Ok(await _repo.GetByNameAsync(name));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody, Required] CustomerDto dto)
        {
            if (dto.EmailConfig is null) return Ok(await _repo.AddAsync(dto.Name));
            else return Ok(await _repo.AddAsync(dto));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequest request)
        {
            return Ok(await _repo.GetTokenAsync(request));
        }
    }
}
