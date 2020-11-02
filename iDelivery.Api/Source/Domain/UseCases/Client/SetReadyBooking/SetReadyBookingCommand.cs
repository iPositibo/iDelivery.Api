using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class SetReadyBookingCommand : IRequest<SetReadyBookingDto>
    {
        public int BookingId { get; set; }

        public SetReadyBookingCommand(int bookingId) => this.BookingId = bookingId;

        private class RequestHandler : IRequestHandler<SetReadyBookingCommand, SetReadyBookingDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<SetReadyBookingDto> Handle(SetReadyBookingCommand request, CancellationToken cancellationToken)
            {
                var booking = await context.Bookings.FindAsync(request.BookingId);
                if (booking == null)
                    throw new NotFoundException();

                var bookingStatus = await context.BookingStatus.FindAsync(booking.BookingStatusId);
                if (bookingStatus == null)
                    throw new NotFoundException();

                if (bookingStatus.BookingStatusName.ToLower() != "cancelled")
                    throw new OnlyStatusCancelledIsAllowedException();

                var status = await context.BookingStatus.SingleOrDefaultAsync(o => o.BookingStatusName.ToLower() == "ready");
                if (status != null)
                    booking.BookingStatusId = status.BookingStatusId;

                context.Update(booking);
                context.SaveChanges();

                return mapper.Map<SetReadyBookingDto>(booking);
            }
        }
    }
}
