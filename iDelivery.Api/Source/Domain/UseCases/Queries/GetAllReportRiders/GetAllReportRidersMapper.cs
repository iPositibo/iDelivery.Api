using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllReportRidersMapper : Profile
    {
        public GetAllReportRidersMapper()
        {
            CreateMap<ReportRider, GetAllRidersDto>();
            CreateMap<GetAllRidersDto, ReportRider>();
        }
    }
}
