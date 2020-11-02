using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetRoleByIdMapper : Profile
    {
        public GetRoleByIdMapper()
        {
            CreateMap<Role, GetRoleByIdDto>();
            CreateMap<GetRoleByIdDto, Role>();
        }
    }
}
