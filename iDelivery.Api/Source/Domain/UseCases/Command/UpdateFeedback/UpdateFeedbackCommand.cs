using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateFeedbackCommand : IRequest
    {
        public int FeedbackId { get; set; }
        public UpdateFeedbackDto Dto { get; }

        public UpdateFeedbackCommand(int feedbackId, UpdateFeedbackDto dto)
        {
            this.FeedbackId = feedbackId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateFeedbackCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Feedback>(request.Dto);

                var feedback = await context.Feedbacks.FindAsync(request.FeedbackId);
                if (feedback == null)
                    throw new NotFoundException();

                // update feedback properties
                feedback.UserId = entity.UserId;
                feedback.Message = entity.Message;

                context.Update(feedback);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
