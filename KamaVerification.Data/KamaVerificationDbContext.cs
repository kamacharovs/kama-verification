using Microsoft.EntityFrameworkCore;
using KamaVerification.Data.Models;

namespace KamaVerification.Data
{
    public class KamaVerificationDbContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
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

                e.HasKey(e => e.CustomerId);

                e.HasQueryFilter(x => !x.IsDeleted);

                e.Property(x => x.CustomerId).ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).ValueGeneratedOnAdd().HasDefaultValueSql("gen_random_uuid()").IsRequired();
                e.Property(x => x.Name).IsRequired();
                e.Property(x => x.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.UpdatedAt).ValueGeneratedOnAdd().ValueGeneratedOnUpdate().HasDefaultValueSql("current_timestamp").IsRequired();
                e.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();

                e.HasOne(x => x.EmailConfig)
                    .WithOne()
                    .HasForeignKey<Customer>(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.NoAction);
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