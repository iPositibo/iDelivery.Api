using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllAvailableCustomerUsersMapper : Profile
    {
        public GetAllAvailableCustomerUsersMapper()
        {
            CreateMap<User, GetAllAvailableCustomerUsersDto>();
            CreateMap<GetAllAvailableCustomerUsersDto, User>();
        }
    }
}
