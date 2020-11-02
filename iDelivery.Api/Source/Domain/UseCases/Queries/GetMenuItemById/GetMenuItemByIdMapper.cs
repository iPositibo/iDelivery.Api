using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetMenuItemByIdMapper : Profile
    {
        public GetMenuItemByIdMapper()
        {
            CreateMap<MenuItem, GetMenuItemByIdDto>();
            CreateMap<GetMenuItemByIdDto, MenuItem>();
        }
    }
}
