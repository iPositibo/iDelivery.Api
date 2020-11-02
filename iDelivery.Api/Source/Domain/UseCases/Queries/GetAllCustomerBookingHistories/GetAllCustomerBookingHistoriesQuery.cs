using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllCustomerBookingHistoriesQuery : IRequest<List<GetAllCustomerBookingHistoriesDto>>
    {
        private class GetAllCustomerBookingHistoriesQueryHandler : IRequestHandler<GetAllCustomerBookingHistoriesQuery, List<GetAllCustomerBookingHistoriesDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllCustomerBookingHistoriesQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllCustomerBookingHistoriesDto>> Handle(GetAllCustomerBookingHistoriesQuery request, CancellationToken cancellationToken)
            {
                var histories = mapper.Map<List<GetAllCustomerBookingHistoriesDto>>(await context.CustomerBookingHistories.ToListAsync());
                foreach (var history in histories)
                {
                    var status = await context.BookingStatus.FindAsync(history.BookingStatusId);
                    if (status != null)
                    {
                        history.BookingStatusName = status.BookingStatusName;
                        history.BookingStatusColor = status.StatusColor;
                    }

                    var customer = await context.Customers.FindAsync(history.CustomerId);
                    if (customer != null)
                        history.CustomerName = $"{ customer.LastName }, { customer.FirstName }";

                    history.BookingDateFormatted = history.BookingDate.ToString("MM/dd/yyyy");
                }

                return histories;
            }
        }
    }
}
