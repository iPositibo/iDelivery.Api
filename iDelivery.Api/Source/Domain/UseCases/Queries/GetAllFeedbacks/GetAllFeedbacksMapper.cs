using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllFeedbacksMapper : Profile
    {
        public GetAllFeedbacksMapper()
        {
            CreateMap<Feedback, GetAllFeedbacksDto>();
            CreateMap<GetAllFeedbacksDto, Feedback>();
        }
    }
}
