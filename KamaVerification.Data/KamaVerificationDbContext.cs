using Microsoft.EntityFrameworkCore;
using KamaVerification.Data.Models;

namespace KamaVerification.Data
{
    public class KamaVerificationDbContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerApiKey> CustomersApiKeys { get; set; }
        public virtual DbSet<CustomerEmailConfig> CustomerEmailConfigs { get; set; }

        public KamaVerificationDbContext(DbContextOptions<KamaVerificationDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(e =>
            {
                e.ToTable("customer");

                e.HasKey(x => x.CustomerId);

                e.HasQueryFilter(x => !x.IsDeleted);

                e.Property(x => x.CustomerId).ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).ValueGeneratedOnAdd().HasDefaultValueSql("gen_random_uuid()").IsRequired();
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.UpdatedAt).ValueGeneratedOnAdd().ValueGeneratedOnUpdate().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.IsDeleted).HasDefaultValueSql("false").IsRequired();

                e.HasOne(x => x.ApiKey)
                    .WithOne()
                    .HasForeignKey<Customer>(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(x => x.EmailConfig)
                    .WithOne()
                    .HasForeignKey<Customer>(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CustomerApiKey>(e =>
            {
                e.ToTable("customer_api_key");

                e.HasKey(x => x.CustomerId);

                e.Property(x => x.CustomerId).ValueGeneratedNever().IsRequired();
                e.Property(x => x.PublicKey).ValueGeneratedOnAdd().HasDefaultValueSql("gen_random_uuid()").IsRequired();
                e.Property(x => x.ApiKey).HasMaxLength(200).IsRequired();
                e.Property(x => x.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.UpdatedAt).ValueGeneratedOnAdd().ValueGeneratedOnUpdate().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.IsEnabled).HasDefaultValueSql("true").IsRequired();
            });

            modelBuilder.Entity<CustomerEmailConfig>(e =>
            {
                e.ToTable("customer_email_config");

                e.HasKey(e => e.CustomerEmailConfigId);

                e.Property(x => x.CustomerEmailConfigId).ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).ValueGeneratedOnAdd().HasDefaultValueSql("gen_random_uuid()").IsRequired();
                e.Property(x => x.CustomerId).IsRequired();
                e.Property(x => x.Subject).HasMaxLength(300).IsRequired();
                e.Property(x => x.FromEmail).HasMaxLength(300).IsRequired();
                e.Property(x => x.FromName).HasMaxLength(300).IsRequired();
                e.Property(x => x.ExpirationInMinutes).HasDefaultValueSql("60").IsRequired();
                e.Property(x => x.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.UpdatedAt).ValueGeneratedOnAdd().ValueGeneratedOnUpdate().HasDefaultValueSql("current_timestamp").IsRequired();
            });
        }
    }
}