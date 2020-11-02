using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateMenuItemMapper : Profile
    {
        public CreateMenuItemMapper()
        {
            CreateMap<MenuItem, CreateMenuItemDto>();
            CreateMap<CreateMenuItemDto, MenuItem>();
        }
    }
}
