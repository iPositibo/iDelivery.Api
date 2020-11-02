using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetRiderByIdMapper : Profile
    {
        public GetRiderByIdMapper()
        {
            CreateMap<Rider, GetRiderByIdDto>();
            CreateMap<GetRiderByIdDto, Rider>();
        }
    }
}
