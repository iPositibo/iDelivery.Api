using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetMenuItemsByRoleIdMapper : Profile
    {
        public GetMenuItemsByRoleIdMapper()
        {
            CreateMap<MenuItem, GetMenuItemsByRoleIdDto>();
            CreateMap<GetMenuItemsByRoleIdDto, MenuItem>();
        }
    }
}
