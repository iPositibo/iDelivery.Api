using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateRiderStatusMapper : Profile
    {
        public UpdateRiderStatusMapper()
        {
            CreateMap<RiderStatus, UpdateRiderStatusDto>();
            CreateMap<UpdateRiderStatusDto, RiderStatus>();
        }
    }
}
