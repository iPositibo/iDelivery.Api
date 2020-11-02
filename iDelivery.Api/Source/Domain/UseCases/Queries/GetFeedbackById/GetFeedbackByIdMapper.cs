using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries.GetFeedbackById
{
    public class GetFeedbackByIdMapper : Profile
    {
        public GetFeedbackByIdMapper()
        {
            CreateMap<Feedback, GetFeedbackByIdDto>();
            CreateMap<GetFeedbackByIdDto, Feedback>();
        }
    }
}
