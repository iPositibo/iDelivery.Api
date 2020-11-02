using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRolesMapper : Profile
    {
        public GetAllRolesMapper()
        {
            CreateMap<Role, GetAllRolesDto>();
            CreateMap<GetAllRolesDto, Role>();
        }
    }
}
