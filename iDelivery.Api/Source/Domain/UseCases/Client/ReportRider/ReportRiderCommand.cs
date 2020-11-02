using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ReportRiderCommand : IRequest<int>
    {
        public ReportRiderDto Dto { get; }

        public ReportRiderCommand(ReportRiderDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<ReportRiderCommand, int>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<int> Handle(ReportRiderCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<ReportRider>(request.Dto);
                context.ReportRiders.Add(entity);
                await context.SaveChangesAsync();

                return entity.ReportRiderId;
            }
        }
    }
}
