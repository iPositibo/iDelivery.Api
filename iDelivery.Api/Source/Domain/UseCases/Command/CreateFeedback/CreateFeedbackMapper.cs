using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateFeedbackMapper : Profile
    {
        public CreateFeedbackMapper()
        {
            CreateMap<Feedback, CreateFeedbackDto>();
            CreateMap<CreateFeedbackDto, Feedback>();
        }
    }
}
