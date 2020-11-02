using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication.ChangePassword
{
    public class ChangePasswordMapper : Profile
    {
        public ChangePasswordMapper()
        {
            CreateMap<User, ChangePasswordDto>();
            CreateMap<ChangePasswordDto, User>();
        }
    }
}
