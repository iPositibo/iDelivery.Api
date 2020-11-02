using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateEstimatedWeightMapper : Profile
    {
        public CreateEstimatedWeightMapper()
        {
            CreateMap<EstimatedWeight, CreateEstimatedWeightDto>();
            CreateMap<CreateEstimatedWeightDto, EstimatedWeight>();
        }
    }
}
