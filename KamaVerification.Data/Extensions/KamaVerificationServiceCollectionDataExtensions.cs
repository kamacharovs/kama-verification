using KamaVerification.Data.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KamaVerification.Data.Extensions
{
    public static class KamaVerificationDataConfigurationExtensions
    {
        public static IServiceCollection AddDataConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var configSection = config.GetConfigSection(KamaVerificationDbOptions.Section);
            var options = configSection.Get<KamaVerificationDbOptions>();

            services.AddDbContext<KamaVerificationDbContext>(o =>
                o.UseLazyLoadingProxies()
                    .UseNpgsql(options.ConnectionString,
                    no =>
                    {
                        no.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        no.MigrationsAssembly("KamaVerification.Data.Migrations");
                    }));

            return services;
        }
    }
}
