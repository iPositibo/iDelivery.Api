using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ActivateDeactivateBooking : IRequest
    {
        public int BookingId { get; set; }
        public bool IsActive { get; set; }

        public ActivateDeactivateBooking(int bookingId, bool isActive) 
        {
            this.BookingId = bookingId;
            this.IsActive = isActive;
        }

        private class RequestHandler : IRequestHandler<ActivateDeactivateBooking>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(ActivateDeactivateBooking request, CancellationToken cancellationToken)
            {
                var booking = await context.Bookings.FindAsync(request.BookingId);
                if (booking == null)
                    throw new NotFoundException();

                booking.IsActive = request.IsActive;
                context.Update(booking);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
