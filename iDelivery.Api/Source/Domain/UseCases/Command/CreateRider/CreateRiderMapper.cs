using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateRiderMapper : Profile
    {
        public CreateRiderMapper()
        {
            CreateMap<Rider, CreateRiderDto>();
            CreateMap<CreateRiderDto, Rider>();
        }
    }
}
