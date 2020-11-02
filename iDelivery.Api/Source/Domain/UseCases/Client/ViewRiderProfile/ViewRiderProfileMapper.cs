using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewRiderProfileMapper : Profile
    {
        public ViewRiderProfileMapper()
        {
            CreateMap<Rider, ViewRiderProfileDto>();
            CreateMap<ViewRiderProfileDto, Rider>();
        }
    }
}
