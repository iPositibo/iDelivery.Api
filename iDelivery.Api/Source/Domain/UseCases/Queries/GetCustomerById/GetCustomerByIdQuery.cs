using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetCustomerByIdQuery : IRequest<GetCustomerByIdDto>
    {
        public int Id { get; }

        public GetCustomerByIdQuery(int id) => this.Id = id;

        private class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetCustomerByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetCustomerByIdDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.Customers.FindAsync(request.Id);
                return mapper.Map<GetCustomerByIdDto>(user);
            }
        }
    }
}
