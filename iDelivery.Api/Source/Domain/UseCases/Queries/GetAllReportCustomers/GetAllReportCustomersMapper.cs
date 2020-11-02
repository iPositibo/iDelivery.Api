using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllReportCustomersMapper : Profile
    {
        public GetAllReportCustomersMapper()
        {
            CreateMap<ReportCustomer, GetAllReportCustomersDto>();
            CreateMap<GetAllReportCustomersDto, ReportCustomer>();
        }
    }
}
