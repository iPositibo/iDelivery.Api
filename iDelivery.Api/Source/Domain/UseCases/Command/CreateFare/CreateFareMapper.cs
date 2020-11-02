using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateFareMapper : Profile
    {
        public CreateFareMapper()
        {
            CreateMap<Fare, CreateFareDto>();
            CreateMap<CreateFareDto, Fare>();
        }
    }
}
