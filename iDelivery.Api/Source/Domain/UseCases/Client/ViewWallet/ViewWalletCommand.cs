using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Domain.UseCases.Client.ViewWallet;
using iDelivery.Api.Source.Domain.UseCases.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewWalletCommand : IRequest<ViewWalletDto>
    {
        public int RiderId { get; set; }

        public ViewWalletCommand(int riderId) => this.RiderId = riderId;

        private class ViewWalletQueryHandler : IRequestHandler<ViewWalletCommand, ViewWalletDto>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public ViewWalletQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<ViewWalletDto> Handle(ViewWalletCommand request, CancellationToken cancellationToken)
            {
                var rider = await context.Riders.SingleOrDefaultAsync(o => o.UserId == request.RiderId);
                if (rider == null)
                    throw new NotFoundException();

                var result = await context.Wallets.FirstOrDefaultAsync(o => o.RiderId == rider.RiderId);
                var wallet = mapper.Map<ViewWalletDto>(result);


                wallet.RiderName = $"{ rider.LastName }, { rider.FirstName }";
                var walletStatus = await context.WalletStatus.FindAsync(wallet.WalletStatusId);
                if (walletStatus != null)
                    wallet.Status = walletStatus.Status;

                var bookingStatus = await context.BookingStatus.SingleOrDefaultAsync(o => o.BookingStatusName.ToLower() == "delivered");
                var bookings = await context.RiderBookingHistories.Where(o => o.RiderId == rider.RiderId && o.BookingStatusId == bookingStatus.BookingStatusId).ToListAsync();
                if (bookings != null)
                {
                    foreach (var booking in bookings)
                    {
                        var deduction = booking.TotalFare * 20 / 100;
                        var riderFare = booking.TotalFare - deduction;
                        booking.RiderFare = riderFare;
                        booking.RiderDeduction = deduction;
                    }
                    wallet.TotalEarnings = bookings.Sum(o => o.RiderFare.GetValueOrDefault()).ToString("0.00");
                }

                wallet.RiderBookings = mapper.Map<List<ViewRiderBookingHistoryDto>>(bookings);
                foreach(var riderBooking in wallet.RiderBookings)
                {
                    riderBooking.RiderFareFormatted = riderBooking.RiderFare.GetValueOrDefault().ToString("0.00");
                    riderBooking.RiderDeductionFormatted = riderBooking.RiderDeduction.GetValueOrDefault().ToString("0.00");
                    riderBooking.TotalFareFormatted = riderBooking.TotalFare.ToString("0.00");
                    riderBooking.BookingDateFormatted = riderBooking.BookingDate.GetValueOrDefault().ToString("MM/dd/yyyy");
                }

                wallet.RiderBookings = wallet.RiderBookings.OrderByDescending(o => o.BookingDate);

                wallet.DateCreatedFormatted = wallet.DateCreated.ToString("MM/dd/yyyy");

                var logs = await context.WalletLogs.Where(o => o.RiderId == wallet.RiderId).ToListAsync();
                if(logs != null)
                    wallet.Logs = mapper.Map<List<GetAllWalletLogsDto>>(logs);

                return wallet;
            }
        }
    }
}
