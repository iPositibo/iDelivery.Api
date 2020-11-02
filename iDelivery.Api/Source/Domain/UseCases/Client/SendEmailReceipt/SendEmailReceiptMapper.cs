using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class SendEmailReceiptMapper : Profile
    {
        public SendEmailReceiptMapper()
        {
            CreateMap<Customer, SendEmailReceiptDto>();
            CreateMap<SendEmailReceiptDto, Customer>();
        }
    }
}
