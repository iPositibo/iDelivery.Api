using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateRiderStatusMapper : Profile
    {
        public CreateRiderStatusMapper()
        {
            CreateMap<RiderStatus, CreateRiderStatusDto>();
            CreateMap<CreateRiderStatusDto, RiderStatus>();
        }
    }
}
