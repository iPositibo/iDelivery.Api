using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetCustomerStatusByIdQuery : IRequest<GetCustomerStatusByIdDto>
    {
        public int Id { get; }

        public GetCustomerStatusByIdQuery(int id) => this.Id = id;

        private class GetCustomerStatusByIdQueryHandler : IRequestHandler<GetCustomerStatusByIdQuery, GetCustomerStatusByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetCustomerStatusByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetCustomerStatusByIdDto> Handle(GetCustomerStatusByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.CustomerStatus.FindAsync(request.Id);
                return mapper.Map<GetCustomerStatusByIdDto>(user);
            }
        }
    }
}
