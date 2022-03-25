using AutoMapper;
using KamaVerification.Data.Dtos;
using KamaVerification.Data.Models;

namespace KamaVerification.Data.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerEmailConfigDto, CustomerEmailConfig>()
                .ForMember(x => x.Subject, o => o.Condition(s => !string.IsNullOrWhiteSpace(s.Subject)))
                .ForMember(x => x.FromEmail, o => o.Condition(s => !string.IsNullOrWhiteSpace(s.FromEmail)))
                .ForMember(x => x.FromName, o => o.Condition(s => !string.IsNullOrWhiteSpace(s.FromName)))
                .ForMember(x => x.ExpirationInMinutes, o => o.Condition(s => s.ExpirationInMinutes.HasValue));

            CreateMap<CustomerDto, Customer>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name));
        }
    }
}
