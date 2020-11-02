using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateRoleMapper : Profile
    {
        public CreateRoleMapper()
        {
            CreateMap<Role, CreateRoleDto>();
            CreateMap<CreateRoleDto, Role>();
        }
    }
}
