using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBookingStatusCommand : IRequest<CreateBookingStatusDto>
    {
        public CreateBookingStatusDto Dto { get; }

        public CreateBookingStatusCommand(CreateBookingStatusDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateBookingStatusCommand, CreateBookingStatusDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateBookingStatusDto> Handle(CreateBookingStatusCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<BookingStatus>(request.Dto);
                context.BookingStatus.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateBookingStatusDto>(entity);
                return result;
            }
        }
    }
}
