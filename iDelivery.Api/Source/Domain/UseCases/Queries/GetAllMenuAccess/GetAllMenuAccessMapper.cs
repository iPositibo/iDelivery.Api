using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllMenuAccessMapper : Profile
    {
        public GetAllMenuAccessMapper()
        {
            CreateMap<MenuAccess, GetAllMenuAccessDto>();
            CreateMap<GetAllMenuAccessDto, MenuAccess>();
        }
    }
}
