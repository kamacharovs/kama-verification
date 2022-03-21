using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace KamaVerification.Data.Extensions
{
    public static class KamaVerificationServiceCollectionDataExtensions
    {
        public static IConfigurationSection GetDbSection(this IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection(KamaVerificationDbOptions.Section);

            if (configurationSection is null)
            {
                throw new ConfigurationErrorsException($"Configuration {KamaVerificationDbOptions.Section} is required");
            }

            return configurationSection;
        }

        public static IServiceCollection AddDataConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var configurationSection = config.GetDbSection();
            var options = configurationSection.Get<KamaVerificationDbOptions>();

            services.AddDbContext<KamaVerificationDbContext>(o =>
                o.UseLazyLoadingProxies()
                    .UseNpgsql(options.ConnectionString,
                    no =>
                    {
                        no.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        no.MigrationsAssembly("KamaVerification.Data.Migration");
                    }));

            return services;
        }
    }
}
