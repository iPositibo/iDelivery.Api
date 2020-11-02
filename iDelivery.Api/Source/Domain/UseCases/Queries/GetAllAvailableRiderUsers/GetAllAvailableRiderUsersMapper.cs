using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllAvailableRiderUsersMapper : Profile
    {
        public GetAllAvailableRiderUsersMapper()
        {
            CreateMap<User, GetAllAvailableRiderUsersDto>();
            CreateMap<GetAllAvailableRiderUsersDto, User>();
        }
    }
}
