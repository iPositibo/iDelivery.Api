using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateMenuItemMapper : Profile
    {
        public UpdateMenuItemMapper()
        {
            CreateMap<MenuItem, UpdateMenuItemDto>();
            CreateMap<UpdateMenuItemDto, MenuItem>();
        }
    }
}
