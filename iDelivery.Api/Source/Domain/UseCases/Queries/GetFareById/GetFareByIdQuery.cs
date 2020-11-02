using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetFareByIdQuery : IRequest<GetFareByIdDto>
    {
        public int Id { get; }

        public GetFareByIdQuery(int id) => this.Id = id;

        private class GetCustomerByIdQueryHandler : IRequestHandler<GetFareByIdQuery, GetFareByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetCustomerByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetFareByIdDto> Handle(GetFareByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.Fares.FindAsync(request.Id);
                return mapper.Map<GetFareByIdDto>(user);
            }
        }
    }
}
