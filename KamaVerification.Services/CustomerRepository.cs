using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using KamaVerification.Data;
using KamaVerification.Data.Exceptions;
using KamaVerification.Data.Models;
using KamaVerification.Data.Dtos;
using FluentValidation;

namespace KamaVerification.Services
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAsync();
        Task<Customer> GetAsync(int customerId);
        Task<Customer> GetAsync(Guid? publicKey);
        Task<Customer> GetByNameAsync(string name);
        Task<Customer> GetAsync(string apiKey);
        Task<TokenResponse> GetTokenAsync(TokenRequest request);
        Task<Customer> AddAsync(string name);
        Task<Customer> AddAsync(CustomerDto dto);
        Task<bool> DeleteAsync(int customerId);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly ITenant _tenant;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepo;
        private readonly IValidator<CustomerDto> _customerDtoValidator;
        private readonly KamaVerificationDbContext _context;

        public CustomerRepository(
            ILogger<CustomerRepository> logger,
            ITenant tenant,
            IMapper mapper,
            ITokenRepository tokenRepo,
            IValidator<CustomerDto> customerDtoValidator,
            KamaVerificationDbContext context)
        {
            _logger = logger;
            _tenant = tenant;
            _mapper = mapper;
            _tokenRepo = tokenRepo;
            _customerDtoValidator = customerDtoValidator;
            _context = context;
        }

        public async Task<Customer> GetAsync()
        {
            return await GetAsync(_tenant.CustomerPublicKey);
        }

        public async Task<Customer> GetAsync(int customerId)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(x => x.CustomerId == customerId)
                ?? throw new KamaVerificationNotFoundException($"Customer with Id={customerId} was not found");
        }
        
        public async Task<Customer> GetAsync(Guid? publicKey)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(x => x.PublicKey == publicKey)
                ?? throw new KamaVerificationNotFoundException($"Customer with PublicKey={publicKey} was not found");
        }

        public async Task<Customer> GetByNameAsync(string name)
        {
            return await _context.Customers
                .Include(x => x.ApiKey)
                .FirstOrDefaultAsync(x => x.Name == name)
                ?? throw new KamaVerificationNotFoundException($"Customer with Name={name} was not found");
        }

        public async Task<Customer> GetAsync(string apiKey)
        {
            return await _context.Customers
                .Include(x => x.ApiKey)
                .FirstOrDefaultAsync(x => x.ApiKey.ApiKey == apiKey);
        }

        public async Task<TokenResponse> GetTokenAsync(TokenRequest request)
        {
            return _tokenRepo.BuildToken(await GetAsync(request.ApiKey));
        }

        public async Task<Customer> AddAsync(string name)
        {
            var isCustomerInDb = await _context.Customers
                .FirstOrDefaultAsync(x => x.Name == name) is not null;

            if (isCustomerInDb) throw new KamaVerificationFriendlyException(HttpStatusCode.BadRequest, "Customer with similar details already exists");
            
            var customer = new Customer { Name = name };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Added customer with name={CustomerName}",
                customer.Name);

            return customer;
        }
        public async Task<Customer> AddAsync(CustomerDto dto)
        {
            await _customerDtoValidator.ValidateAndThrowAsync(dto);

            var isCustomerInDb = await _context.Customers
                .FirstOrDefaultAsync(x => x.Name == dto.Name
                    && x.EmailConfig.Subject == dto.EmailConfig.Subject
                    && x.EmailConfig.FromEmail == dto.EmailConfig.FromEmail
                    && x.EmailConfig.FromName == dto.EmailConfig.FromName
                    && x.EmailConfig.ExpirationInMinutes == dto.EmailConfig.ExpirationInMinutes) is not null;

            if (isCustomerInDb) throw new KamaVerificationFriendlyException(HttpStatusCode.BadRequest, "Customer with similar details already exists");

            var customer = _mapper.Map<Customer>(dto);
            customer.ApiKey = dto.GenerateApiKey ? new CustomerApiKey { ApiKey = GenerateApiKey() } : null;

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
