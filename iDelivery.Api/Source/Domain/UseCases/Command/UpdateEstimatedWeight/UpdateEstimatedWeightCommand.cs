using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateEstimatedWeightCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateEstimatedWeightDto Dto { get; }

        public UpdateEstimatedWeightCommand(int id, UpdateEstimatedWeightDto dto)
        {
            this.Id = id;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateEstimatedWeightCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateEstimatedWeightCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<EstimatedWeight>(request.Dto);

                var estimatedWeight = await context.EstimatedWeights.FindAsync(request.Id);
                if (estimatedWeight == null)
                    throw new NotFoundException();

                // update estimated weight properties
                estimatedWeight.Value = entity.Value;

                context.Update(estimatedWeight);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
