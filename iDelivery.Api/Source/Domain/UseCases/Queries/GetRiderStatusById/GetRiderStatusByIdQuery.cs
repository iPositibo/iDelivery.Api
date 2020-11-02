using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetRiderStatusByIdQuery : IRequest<GetRiderStatusByIdDto>
    {
        public int Id { get; }

        public GetRiderStatusByIdQuery(int id) => this.Id = id;

        private class GetRiderStatusByIdQueryHandler : IRequestHandler<GetRiderStatusByIdQuery, GetRiderStatusByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetRiderStatusByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetRiderStatusByIdDto> Handle(GetRiderStatusByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.RiderStatus.FindAsync(request.Id);
                return mapper.Map<GetRiderStatusByIdDto>(user);
            }
        }
    }
}
