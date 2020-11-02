using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateMenuAccessMapper : Profile
    {
        public CreateMenuAccessMapper()
        {
            CreateMap<MenuAccess, CreateMenuAccessDto>();
            CreateMap<CreateMenuAccessDto, MenuAccess>();
        }
    }
}
