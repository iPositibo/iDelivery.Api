using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RegisterNumberMapper : Profile
    {
        public RegisterNumberMapper()
        {
            CreateMap<Otpregistration, RegisterNumberDto>();
            CreateMap<RegisterNumberDto, Otpregistration>();
        }
    }
}
