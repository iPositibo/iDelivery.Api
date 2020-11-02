using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateRiderBookingHistoryCommand : IRequest<CreateRiderBookingHistoryDto>
    {
        public CreateRiderBookingHistoryDto Dto { get; }

        public CreateRiderBookingHistoryCommand(CreateRiderBookingHistoryDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateRiderBookingHistoryCommand, CreateRiderBookingHistoryDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateRiderBookingHistoryDto> Handle(CreateRiderBookingHistoryCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<RiderBookingHistory>(request.Dto);
                context.RiderBookingHistories.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateRiderBookingHistoryDto>(entity);
                return result;
            }
        }
    }
}
