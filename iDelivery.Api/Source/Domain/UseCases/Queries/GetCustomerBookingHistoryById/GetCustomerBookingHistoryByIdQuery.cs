using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetCustomerBookingHistoryByIdQuery : IRequest<GetCustomerBookingHistoryByIdDto>
    {
        public int Id { get; }

        public GetCustomerBookingHistoryByIdQuery(int id) => this.Id = id;

        private class GetCustomerBookingHistoryByIdQueryHandler : IRequestHandler<GetCustomerBookingHistoryByIdQuery, GetCustomerBookingHistoryByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetCustomerBookingHistoryByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetCustomerBookingHistoryByIdDto> Handle(GetCustomerBookingHistoryByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.CustomerBookingHistories.FindAsync(request.Id);
                var history = mapper.Map<GetCustomerBookingHistoryByIdDto>(result);

                var status = await context.BookingStatus.FindAsync(history.BookingStatusId);
                if (status != null)
                    history.BookingStatusName = status.BookingStatusName;

                var customer = await context.Customers.FindAsync(history.CustomerId);
                if (customer != null)
                    history.CustomerName = $"{ customer.LastName }, { customer.FirstName }";

                history.BookingDateFormatted = history.BookingDate.ToString("MM/dd/yyyy");

                return history;
            }
        }
    }
}
