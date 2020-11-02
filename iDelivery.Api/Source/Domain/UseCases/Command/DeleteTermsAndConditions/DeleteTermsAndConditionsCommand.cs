using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteTermsAndConditionsCommand : IRequest
    {
        public int TermsAndConditionId { get; set; }

        public DeleteTermsAndConditionsCommand(int termsAndConditionId) => this.TermsAndConditionId = termsAndConditionId;

        private class RequestHandler : IRequestHandler<DeleteTermsAndConditionsCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteTermsAndConditionsCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.TermsAndConditions.FindAsync(request.TermsAndConditionId);
                if (entity == null)
                    throw new NotFoundException();

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
