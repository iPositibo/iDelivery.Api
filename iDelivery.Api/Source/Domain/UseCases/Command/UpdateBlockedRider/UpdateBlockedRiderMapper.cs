using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBlockedRiderMapper : Profile
    {
        public UpdateBlockedRiderMapper()
        {
            CreateMap<BlockedRider, UpdateBlockedRiderDto>();
            CreateMap<UpdateBlockedRiderDto, BlockedRider>();
        }
    }
}
