﻿using KamaVerification.Data.Dtos;
using KamaVerification.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody, Required] CustomerDto dto)
        {
            return Ok(await _repo.AddAsync(dto));
        }
    }
}