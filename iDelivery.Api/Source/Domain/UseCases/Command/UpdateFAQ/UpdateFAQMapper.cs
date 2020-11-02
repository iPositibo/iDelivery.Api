using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateFAQMapper : Profile
    {
        public UpdateFAQMapper()
        {
            CreateMap<Faq, UpdateFAQDto>();
            CreateMap<UpdateFAQDto, Faq>();
        }
    }
}
