using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateMenuAccessMapper : Profile
    {
        public UpdateMenuAccessMapper()
        {
            CreateMap<MenuAccess, UpdateMenuAccessDto>();
            CreateMap<UpdateMenuAccessDto, MenuAccess>();
        }
    }
}
