using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllEstimatedWeightsMapper : Profile
    {
        public GetAllEstimatedWeightsMapper()
        {
            CreateMap<EstimatedWeight, GetAllEstimatedWeightsDto>();
            CreateMap<GetAllEstimatedWeightsDto, EstimatedWeight>();
        }
    }
}
