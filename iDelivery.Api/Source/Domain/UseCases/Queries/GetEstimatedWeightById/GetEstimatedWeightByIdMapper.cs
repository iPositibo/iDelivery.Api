using AutoMapper;
using iDelivery.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetEstimatedWeightByIdMapper : Profile
    {
        public GetEstimatedWeightByIdMapper()
        {
            CreateMap<EstimatedWeight, GetEstimatedWeightByIdDto>();
            CreateMap<GetEstimatedWeightByIdDto, EstimatedWeight>();
        }
    }
}
