using KamaVerification.Data;
using KamaVerification.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KamaVerification.Services
{
    public interface IVerificationRepository
    {
        string GenerateCode(int length = 6);
    }

    public class VerificationRepository : IVerificationRepository
    {
        private readonly ILogger<VerificationRepository> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly KamaVerificationDbContext _context;

        private Random _random = new Random();
        private const string _digits = "0123456789";

        public VerificationRepository(
            ILogger<VerificationRepository> logger,
            ICustomerRepository customerRepository,
            KamaVerificationDbContext context)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _context = context;
        }

        public string GenerateCode(int length = 6)
        {
            return new string(Enumerable.Repeat(_digits, length)
                .Select(s => s[_random.Next(length)]).ToArray());
        }
    }
}