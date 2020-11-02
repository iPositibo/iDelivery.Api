using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetMenuAccessByMapper : Profile
    {
        public GetMenuAccessByMapper()
        {
            CreateMap<MenuAccess, GetAllMenuAccessDto>();
            CreateMap<GetAllMenuAccessDto, MenuAccess>();
        }
    }
}
