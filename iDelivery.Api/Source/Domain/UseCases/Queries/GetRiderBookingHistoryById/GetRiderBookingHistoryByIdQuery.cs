using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetRiderBookingHistoryByIdQuery : IRequest<GetRiderBookingHistoryByIdDto>
    {
        public int Id { get; }

        public GetRiderBookingHistoryByIdQuery(int id) => this.Id = id;

        private class GetRiderBookingHistoryByIdQueryHandler : IRequestHandler<GetRiderBookingHistoryByIdQuery, GetRiderBookingHistoryByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetRiderBookingHistoryByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetRiderBookingHistoryByIdDto> Handle(GetRiderBookingHistoryByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.RiderBookingHistories.FindAsync(request.Id);
                return mapper.Map<GetRiderBookingHistoryByIdDto>(user);
            }
        }
    }
}
