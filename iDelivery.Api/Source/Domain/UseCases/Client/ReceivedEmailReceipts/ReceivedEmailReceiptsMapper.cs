using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ReceivedEmailReceiptsMapper : Profile
    {
        public ReceivedEmailReceiptsMapper()
        {
            CreateMap<Customer, ReceivedEmailReceiptsDto>();
            CreateMap<ReceivedEmailReceiptsDto, Customer>();

            CreateMap<Rider, ReceivedEmailReceiptsDto>();
            CreateMap<ReceivedEmailReceiptsDto, Rider>();
        }
    }
}
