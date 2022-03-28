using KamaVerification.Data;
using KamaVerification.Data.Models;
using KamaVerification.Data.Dtos;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace KamaVerification.Services
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAsync(int customerId);
        Task<Customer> GetAsync(string apiKey);
        Task<Customer> AddAsync(CustomerDto dto);
        Task<bool> DeleteAsync(int customerId);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly IMapper _mapper;
        private readonly KamaVerificationDbContext _context;

        public CustomerRepository(
            ILogger<CustomerRepository> logger,
            IMapper mapper,
            KamaVerificationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Customer> GetAsync(int customerId)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }

        public async Task<Customer> GetAsync(string apiKey)
        {
            return await _context.Customers
                .Include(x => x.ApiKey)
                .FirstOrDefaultAsync(x => x.ApiKey.ApiKey == apiKey);
        }

        public async Task<Customer> AddAsync(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Added customer with name={CustomerName}",
                customer.Name);

            return customer;
        }

        public async Task<bool> DeleteAsync(int customerId)
        {
            var customer = await GetAsync(customerId);
            customer.IsDeleted = true;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted customer with name={CustomerName}",
                customer.Name);

            return true;
        }

        public string GenerateApiKey(int length = 32)
        {
            var key = new byte[length];
            using (var generator = RandomNumberGenerator.Create()) generator.GetBytes(key);
            
            return Convert.ToBase64String(key);
        }
    }
}
