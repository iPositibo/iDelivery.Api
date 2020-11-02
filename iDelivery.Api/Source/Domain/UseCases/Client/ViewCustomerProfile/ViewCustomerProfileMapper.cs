using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewCustomerProfileMapper : Profile
    {
        public ViewCustomerProfileMapper()
        {
            CreateMap<Customer, ViewCustomerProfileDto>();
            CreateMap<ViewCustomerProfileDto, Customer>();
        }
    }
}
