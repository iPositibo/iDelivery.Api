using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class VerifyCodeMapper : Profile
    {
        public VerifyCodeMapper()
        {
            CreateMap<Customer, VerifyCodeDto>();
            CreateMap<VerifyCodeDto, Customer>();
        }
    }
}
