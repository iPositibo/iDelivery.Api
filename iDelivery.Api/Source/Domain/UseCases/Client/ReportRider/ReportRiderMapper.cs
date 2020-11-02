using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ReportRiderMapper : Profile
    {
        public ReportRiderMapper()
        {
            CreateMap<ReportRider, ReportRiderDto>();
            CreateMap<ReportRiderDto, ReportRider>();
        }
    }
}
