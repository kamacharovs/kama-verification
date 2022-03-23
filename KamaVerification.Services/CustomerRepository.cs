using KamaVerification.Data;
using KamaVerification.Data.Models;
using KamaVerification.Data.Dtos;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace KamaVerification.Services
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(CustomerDto dto);
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

        public async Task<Customer> AddAsync(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
