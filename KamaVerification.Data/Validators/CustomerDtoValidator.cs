using FluentValidation;
using KamaVerification.Data.Dtos;

namespace KamaVerification.Data.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.EmailConfig.ExpirationInMinutes)
                .GreaterThanOrEqualTo(1)
                .When(x => x.EmailConfig != null);
        }
    }
}
