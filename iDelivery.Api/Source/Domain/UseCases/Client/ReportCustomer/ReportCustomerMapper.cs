using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ReportCustomerMapper : Profile
    {
        public ReportCustomerMapper()
        {
            CreateMap<ReportCustomer, ReportCustomerDto>();
            CreateMap<ReportCustomerDto, ReportCustomer>();
        }
    }
}
