using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateRiderMapper : Profile
    {
        public UpdateRiderMapper()
        {
            CreateMap<Rider, UpdateRiderDto>();
            CreateMap<UpdateRiderDto, Rider>();
        }
    }
}
