using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateTermsAndConditionsCommand : IRequest
    {
        public int UserId { get; set; }
        public UpdateTermsAndConditionsDto Dto { get; }

        public UpdateTermsAndConditionsCommand(int userId, UpdateTermsAndConditionsDto dto)
        {
            this.UserId = userId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateTermsAndConditionsCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateTermsAndConditionsCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<TermsAndCondition>(request.Dto);

                var termsAndCondition = await context.TermsAndConditions.FindAsync(request.UserId);
                if (termsAndCondition == null)
                    throw new NotFoundException();

                // update terms and condition properties
                termsAndCondition.Content = entity.Content;

                context.Update(termsAndCondition);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
