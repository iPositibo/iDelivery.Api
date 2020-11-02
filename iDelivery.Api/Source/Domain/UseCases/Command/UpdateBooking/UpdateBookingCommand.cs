using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Infrastructure.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBookingCommand : IRequest
    {
        public int BookingId { get; set; }
        public UpdateBookingDto Dto { get; }

        public UpdateBookingCommand(int bookingId, UpdateBookingDto dto)
        {
            this.BookingId = bookingId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateBookingCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Booking>(request.Dto);

                var booking = await context.Bookings.FindAsync(request.BookingId);
                if (booking == null)
                    throw new NotFoundException();

                // update booking properties
                booking.BookingStatusId = entity.BookingStatusId;
                booking.BookingDate = entity.BookingDate;
                booking.ContactName = entity.ContactName;
                booking.ContactNumber = entity.ContactNumber;
                booking.DropOffLocation = entity.DropOffLocation;
                booking.DropOffLatitude = entity.DropOffLatitude;
                booking.DropOffLongitude = entity.DropOffLongitude;
                booking.DropOffTime = entity.DropOffTime;
                booking.Items = entity.Items;
                booking.Notes = entity.Notes;
                booking.TotalEstimatedWeight = entity.TotalEstimatedWeight;
                booking.IsActive = entity.IsActive;
                booking.PickupLocation = entity.PickupLocation;
                booking.PickupLongitude = entity.PickupLongitude;
                booking.PickupLatitude = entity.PickupLatitude;
                booking.PickupTime = entity.PickupTime;
                booking.FareId = entity.FareId;
                booking.RiderId = entity.RiderId;
                booking.PhotoUrl = entity.PhotoUrl;

                context.Update(booking);
                context.SaveChanges();

                // log in booking history
                var customerBookingHistory = new CustomerBookingHistory();
                customerBookingHistory.CustomerId = booking.CustomerId;
                customerBookingHistory.BookingStatusId = booking.BookingStatusId;
                customerBookingHistory.ReceiverCompleteName = booking.ContactName;
                customerBookingHistory.ReceiverCompleteAddress = booking.DropOffLocation;
                customerBookingHistory.EstimatedTime = booking.EstimatedTime;
                customerBookingHistory.ItemDetails = booking.Items;
                customerBookingHistory.TotalKilometers = booking.TotalKilometers;
                customerBookingHistory.Receipt = booking.ReceiptNumber;
                customerBookingHistory.BookingDate = booking.BookingDate;

                var fare = await context.Fares.FindAsync(booking.FareId);
                if (fare != null)
                {
                    if (!string.IsNullOrEmpty(entity.TotalKilometers))
                        customerBookingHistory.TotalFare = FareHelper.Compute(fare.PricePerKilometer, fare.BaseFare, fare.Surcharge, GetKilometer(booking.TotalKilometers));
                }

                context.CustomerBookingHistories.Add(customerBookingHistory);
                await context.SaveChangesAsync();

                return await Task.FromResult(Unit.Value);
            }

            private int GetKilometer(string kilometer)
            {
                var b = string.Empty;
                var val = 0;
                for (int i = 0; i < kilometer.Length; i++)
                {
                    if (Char.IsDigit(kilometer[i]))
                        b += kilometer[i];
                }

                if (b.Length > 0)
                    val = int.Parse(b);

                return val;
            }
        }
    }
}
