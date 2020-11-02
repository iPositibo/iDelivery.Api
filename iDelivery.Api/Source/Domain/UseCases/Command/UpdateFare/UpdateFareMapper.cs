using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateFareMapper : Profile
    {
        public UpdateFareMapper()
        {
            CreateMap<Fare, UpdateFareDto>();
            CreateMap<UpdateFareDto, Fare>();
        }
    }
}
