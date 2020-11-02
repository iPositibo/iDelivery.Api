using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBookingHistoriesQuery : IRequest<List<GetAllBookingHistoriesDto>>
    {
        private class GetAllBookingHistoriesQueryHandler : IRequestHandler<GetAllBookingHistoriesQuery, List<GetAllBookingHistoriesDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllBookingHistoriesQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllBookingHistoriesDto>> Handle(GetAllBookingHistoriesQuery request, CancellationToken cancellationToken)
            {
                var histories = mapper.Map<List<GetAllBookingHistoriesDto>>(await context.BookingHistories.ToListAsync());
                foreach (var history in histories)
                {
                    var status = await context.BookingStatus.FindAsync(history.BookingStatusId);
                    if (status != null)
                        history.BookingStatusName = status.BookingStatusName;

                    var customer = await context.Customers.FindAsync(history.CustomerId);
                    if (customer != null)
                        history.CustomerName = $"{ customer.LastName }, { customer.FirstName }";
                }

                return histories;
            }
        }
    }
}
