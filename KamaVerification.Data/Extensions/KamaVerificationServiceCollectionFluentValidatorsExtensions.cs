using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using KamaVerification.Data.Validators;

namespace KamaVerification.Data.Extensions
{
    public static class KamaVerificationServiceCollectionFluentValidatorsExtensions
    {
        public static IServiceCollection AddFluentValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CustomerDtoValidator>(ServiceLifetime.Singleton);

            return services;
        }
    }
}
