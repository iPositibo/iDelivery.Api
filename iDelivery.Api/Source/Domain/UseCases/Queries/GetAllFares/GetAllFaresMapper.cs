using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllFaresMapper : Profile
    {
        public GetAllFaresMapper()
        {
            CreateMap<Fare, GetAllFaresDto>();
            CreateMap<GetAllFaresDto, Fare>();
        }
    }
}
