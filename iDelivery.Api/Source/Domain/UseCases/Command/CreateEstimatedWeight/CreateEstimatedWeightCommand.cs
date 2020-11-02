using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateEstimatedWeightCommand : IRequest<CreateEstimatedWeightDto>
    {
        public CreateEstimatedWeightDto Dto { get; }

        public CreateEstimatedWeightCommand(CreateEstimatedWeightDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateEstimatedWeightCommand, CreateEstimatedWeightDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateEstimatedWeightDto> Handle(CreateEstimatedWeightCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<EstimatedWeight>(request.Dto);

                context.EstimatedWeights.Add(entity);
                await context.SaveChangesAsync();

                return mapper.Map<CreateEstimatedWeightDto>(entity);
            }
        }
    }
}
