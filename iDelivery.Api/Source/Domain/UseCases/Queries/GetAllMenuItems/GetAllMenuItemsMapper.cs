using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllMenuItemsMapper : Profile
    {
        public GetAllMenuItemsMapper()
        {
            CreateMap<MenuItem, GetAllMenuItemsDto>();
            CreateMap<GetAllMenuItemsDto, MenuItem>();
        }
    }
}
