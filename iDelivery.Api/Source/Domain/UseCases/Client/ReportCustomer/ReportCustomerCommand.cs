using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ReportCustomerCommand : IRequest<int>
    {
        public ReportCustomerDto Dto { get; }

        public ReportCustomerCommand(ReportCustomerDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<ReportCustomerCommand, int>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<int> Handle(ReportCustomerCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<ReportCustomer>(request.Dto);
                context.ReportCustomers.Add(entity);
                await context.SaveChangesAsync();

                return entity.ReportCustomerId;
            }
        }
    }
}
