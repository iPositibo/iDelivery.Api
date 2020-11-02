using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetBookingHistoryByIdQuery : IRequest<GetBookingHistoryByIdDto>
    {
        public int Id { get; }

        public GetBookingHistoryByIdQuery(int id) => this.Id = id;

        private class GetBookingHistoryByIdQueryHandler : IRequestHandler<GetBookingHistoryByIdQuery, GetBookingHistoryByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetBookingHistoryByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetBookingHistoryByIdDto> Handle(GetBookingHistoryByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.BookingHistories.FindAsync(request.Id);
                return mapper.Map<GetBookingHistoryByIdDto>(user);
            }
        }
    }
}
