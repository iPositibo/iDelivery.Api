using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetRiderStatusByIdMapper : Profile
    {
        public GetRiderStatusByIdMapper()
        {
            CreateMap<RiderStatus, GetRiderStatusByIdDto>();
            CreateMap<GetRiderStatusByIdDto, RiderStatus>();
        }
    }
}
