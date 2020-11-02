using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteFaqCommand : IRequest
    {
        public int FaqId { get; set; }

        public DeleteFaqCommand(int faqId) => this.FaqId = faqId;

        private class RequestHandler : IRequestHandler<DeleteFaqCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteFaqCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.Faqs.FindAsync(request.FaqId);
                if (entity == null)
                    throw new NotFoundException();

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
