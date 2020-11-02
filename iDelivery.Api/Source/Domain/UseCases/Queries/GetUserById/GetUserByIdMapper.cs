using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetUserByIdMapper : Profile
    {
        public GetUserByIdMapper()
        {
            CreateMap<User, GetUserByIdDto>();
        }
    }
}
