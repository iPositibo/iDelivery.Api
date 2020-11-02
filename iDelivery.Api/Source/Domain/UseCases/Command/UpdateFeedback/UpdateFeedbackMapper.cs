using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateFeedbackMapper : Profile
    {
        public UpdateFeedbackMapper()
        {
            CreateMap<Feedback, UpdateFeedbackDto>();
            CreateMap<UpdateFeedbackDto, Feedback>();
        }
    }
}
