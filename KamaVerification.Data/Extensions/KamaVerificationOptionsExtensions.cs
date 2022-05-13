using KamaVerification.Data.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace KamaVerification.Data.Extensions
{
    public static partial class KamaVerificationOptionsExtensions
    {
        public static IConfigurationSection GetConfigSection(this IConfiguration configuration, string sectionName)
        {
            var configSection = configuration.GetSection(sectionName);

            if (configSection is null)
            {
                throw new ConfigurationErrorsException($"Configuration {sectionName} is required");
            }

            return configSection;
        }
    }
}
