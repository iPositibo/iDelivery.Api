using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewOnlineRidersMapper : Profile
    {
        public ViewOnlineRidersMapper()
        {
            CreateMap<Rider, ViewOnlineRidersDto>();
            CreateMap<ViewOnlineRidersDto, Rider>();
        }
    }
}
