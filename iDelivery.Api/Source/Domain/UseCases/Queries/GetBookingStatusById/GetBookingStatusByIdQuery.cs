using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetBookingStatusByIdQuery : IRequest<GetBookingStatusByIdDto>
    {
        public int Id { get; }

        public GetBookingStatusByIdQuery(int id) => this.Id = id;

        private class GetBookingStatusByIdQueryHandler : IRequestHandler<GetBookingStatusByIdQuery, GetBookingStatusByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetBookingStatusByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetBookingStatusByIdDto> Handle(GetBookingStatusByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.BookingStatus.FindAsync(request.Id);
                return mapper.Map<GetBookingStatusByIdDto>(user);
            }
        }
    }
}
