using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateEstimatedWeightMapper : Profile
    {
        public UpdateEstimatedWeightMapper()
        {
            CreateMap<EstimatedWeight, UpdateEstimatedWeightDto>();
            CreateMap<UpdateEstimatedWeightDto, EstimatedWeight>();
        }
    }
}
