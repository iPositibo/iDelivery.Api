using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteFeedbackCommand : IRequest
    {
        public int FeedbackId { get; set; }

        public DeleteFeedbackCommand(int feedbackId) => this.FeedbackId = feedbackId;

        private class RequestHandler : IRequestHandler<DeleteFeedbackCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.Feedbacks.FindAsync(request.FeedbackId);
                if (entity == null)
                    throw new NotFoundException();

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
