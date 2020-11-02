using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRiderBookingHistoriesQuery : IRequest<List<GetAllRiderBookingHistoriesDto>>
    {
        private class GetAllRiderBookingHistoriesQueryHandler : IRequestHandler<GetAllRiderBookingHistoriesQuery, List<GetAllRiderBookingHistoriesDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllRiderBookingHistoriesQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllRiderBookingHistoriesDto>> Handle(GetAllRiderBookingHistoriesQuery request, CancellationToken cancellationToken)
            {
                var histories = mapper.Map<List<GetAllRiderBookingHistoriesDto>>(await context.RiderBookingHistories.ToListAsync());
                foreach (var history in histories)
                {
                    var status = await context.BookingStatus.FindAsync(history.BookingStatusId);
                    if (status != null)
                    {
                        history.BookingStatusName = status.BookingStatusName;
                        history.BookingStatusColor = status.StatusColor;
                    }

                    var rider = await context.Riders.FindAsync(history.RiderId);
                    if (rider != null)
                        history.RiderName = $"{ rider.LastName }, { rider.FirstName }";
                }

                return histories;
            }
        }
    }
}
