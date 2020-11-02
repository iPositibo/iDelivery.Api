using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class SetOnlineRiderMapper : Profile
    {
        public SetOnlineRiderMapper()
        {
            CreateMap<Rider, SetOnlineRiderDto>();
            CreateMap<SetOnlineRiderDto, Rider>();
        }
    }
}
