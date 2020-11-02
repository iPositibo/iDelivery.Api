using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetBookingByIdQuery : IRequest<GetBookingByIdDto>
    {
        public int Id { get; }

        public GetBookingByIdQuery(int id) => this.Id = id;

        private class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, GetBookingByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetBookingByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetBookingByIdDto> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.Bookings.FindAsync(request.Id);
                if (result == null)
                    throw new NotFoundException();

                var booking = mapper.Map<GetBookingByIdDto>(result);

                var status = await context.BookingStatus.FindAsync(booking.BookingStatusId);
                if (status != null)
                    booking.BookingStatusName = status.BookingStatusName;

                var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == booking.CustomerId);
                if (customer != null)
                    booking.CustomerName = $"{ customer.LastName }, {customer.FirstName}";

                var rider = await context.Riders.FindAsync(booking.RiderId);
                if (rider != null)
                    booking.RiderName = $"{ rider.LastName }, {rider.FirstName}";

                booking.BookingDateFormatted = booking.BookingDate.ToString("MM/dd/yyyy");
                booking.PickupTimeFormatted = booking.PickupTime.GetValueOrDefault().ToString("hh:mm tt");
                booking.DropOffTimeFormatted = booking.DropOffTime.GetValueOrDefault().ToString("hh:mm tt");

                return booking;
            }
        }
    }
}
