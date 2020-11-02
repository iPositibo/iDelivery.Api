using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateRoleMapper : Profile
    {
        public UpdateRoleMapper()
        {
            CreateMap<Role, UpdateRoleDto>();
            CreateMap<UpdateRoleDto, Role>();
        }
    }
}
