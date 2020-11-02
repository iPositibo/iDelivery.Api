using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetMenuAccessByRoleIdMapper : Profile
    {
        public GetMenuAccessByRoleIdMapper()
        {
            CreateMap<MenuAccess, GetMenuAccessByRoleIdDto>();
            CreateMap<GetMenuAccessByRoleIdDto, MenuAccess>();
        }
    }
}
