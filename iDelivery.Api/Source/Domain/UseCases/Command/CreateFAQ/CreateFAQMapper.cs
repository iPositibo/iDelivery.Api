using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateFAQMapper : Profile
    {
        public CreateFAQMapper()
        {
            CreateMap<Faq, CreateFAQDto>();
            CreateMap<CreateFAQDto, Faq>();
        }
    }
}
