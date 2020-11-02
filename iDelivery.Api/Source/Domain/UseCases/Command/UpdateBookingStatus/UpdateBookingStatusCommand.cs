using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBookingStatusCommand : IRequest
    {
        public int BookingStatusId { get; set; }
        public UpdateBookingStatusDto Dto { get; }

        public UpdateBookingStatusCommand(int bookingStatusId, UpdateBookingStatusDto dto)
        {
            this.BookingStatusId = bookingStatusId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateBookingStatusCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateBookingStatusCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<BookingStatus>(request.Dto);

                var bookingStatus = await context.BookingStatus.FindAsync(request.BookingStatusId);
                if (bookingStatus == null)
                    throw new NotFoundException();

                // update booking status properties
                bookingStatus.BookingStatusName = entity.BookingStatusName;
                bookingStatus.StatusColor = entity.StatusColor;

                context.Update(bookingStatus);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
