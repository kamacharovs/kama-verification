using KamaVerification.Data.Models;
using System.Linq;

namespace KamaVerification.Data.Migrations
{
    public class FakeDataManager
    {
        private readonly KamaVerificationDbContext _context;

        public FakeDataManager(KamaVerificationDbContext context)
        {
            _context = context;
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    PublicKey = Guid.NewGuid(),
                    Name = "Wayne Enterprise",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Customer
                {
                    CustomerId = 2,
                    PublicKey = Guid.NewGuid(),
                    Name = "Kama Verification Inc.",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
        }

        private IEnumerable<CustomerEmailConfig> GetCustomerEmailConfigs()
        {
            return new List<CustomerEmailConfig>
            {
                new CustomerEmailConfig
                {
                    CustomerEmailConfigId = 1,
                    CustomerId = 1,
                    Subject = "Your Wayne Enterprise verification code",
                    FromEmail = "yifet83692@sofrge.com",
                    FromName = "Wayne Enterprise",
                    ExpirationInMinutes = 15,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new CustomerEmailConfig
                {
                    CustomerEmailConfigId = 2,
                    CustomerId = 2,
                    Subject = "Your Kama Verification Inc. verification code",
                    FromEmail = "yifet83692@sofrge.com",
                    FromName = "Kama Verification Inc.",
                    ExpirationInMinutes = 60,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
        }

        public async Task SeedDataAsync()
        {
            await _context.Customers.AddRangeAsync(GetCustomers());
            await _context.CustomerEmailConfigs.AddRangeAsync(GetCustomerEmailConfigs());

            await _context.SaveChangesAsync();
        }

        public void SeedData()
        {
            _context.Customers.AddRange(GetCustomers());
            _context.CustomerEmailConfigs.AddRange(GetCustomerEmailConfigs());

            _context.SaveChanges();
        }
    }
}
