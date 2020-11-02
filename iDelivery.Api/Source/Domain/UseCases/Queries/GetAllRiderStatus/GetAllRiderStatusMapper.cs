using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRiderStatusMapper : Profile
    {
        public GetAllRiderStatusMapper()
        {
            CreateMap<RiderStatus, GetAllRiderStatusDto>();
            CreateMap<GetAllRiderStatusDto, RiderStatus>();
        }
    }
}
