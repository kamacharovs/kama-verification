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

        private IEnumerable<CustomerRole> GetCustomerRoles()
        {
            return new List<CustomerRole>
            {
                new CustomerRole
                {
                    RoleName = "customer"
                },
                new CustomerRole
                {
                    RoleName = "admin"
                }
            };
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    Name = "Wayne Enterprise",
                    RoleName = "customer",
                    ApiKey = new CustomerApiKey
                    {
                        CustomerId = 1,
                        ApiKey = "g1Ji6nKa2RfUlBjOgzZYnt9rFYnB3FjV"
                    },
                    EmailConfig = new CustomerEmailConfig
                    {
                        CustomerId = 1,
                        Subject = "Your Wayne Enterprise verification code",
                        FromEmail = "yifet83692@sofrge.com",
                        FromName = "Wayne Enterprise",
                        ExpirationInMinutes = 15
                    }
                },
                new Customer
                {
                    CustomerId = 2,
                    Name = "Kama Verification Inc.",
                    RoleName = "admin",
                    ApiKey = new CustomerApiKey
                    {
                        CustomerId = 2,
                        ApiKey = "g1Ji6nKa2RfUlBjOgzZYnt9rFYnB3FjVs"
                    },
                    EmailConfig = new CustomerEmailConfig
                    {
                        CustomerId = 2,
                        Subject = "Your Kama Verification Inc. verification code",
                        FromEmail = "yifet83692@sofrge.com",
                        FromName = "Kama Verification Inc.",
                        ExpirationInMinutes = 60
                    }
                }
            };
        }

        public async Task SeedDataAsync()
        {
            await _context.CustomerRoles.AddRangeAsync(GetCustomerRoles());
            await _context.Customers.AddRangeAsync(GetCustomers());

            await _context.SaveChangesAsync();
        }

        public void SeedData()
        {
            _context.CustomerRoles.AddRange(GetCustomerRoles());
            _context.Customers.AddRange(GetCustomers());

            _context.SaveChanges();
        }
    }
}
