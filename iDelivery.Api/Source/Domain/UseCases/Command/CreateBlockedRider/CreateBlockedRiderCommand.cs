using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBlockedRiderCommand : IRequest<CreateBlockedRiderDto>
    {
        public CreateBlockedRiderDto Dto { get; }

        public CreateBlockedRiderCommand(CreateBlockedRiderDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateBlockedRiderCommand, CreateBlockedRiderDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateBlockedRiderDto> Handle(CreateBlockedRiderCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<BlockedRider>(request.Dto);

                // blocked rider
                var rider = await context.Riders.FindAsync(request.Dto.RiderId);
                if (rider != null)
                {
                    var blockedRider = await context.BlockedRiders.SingleOrDefaultAsync(o => o.RiderId == request.Dto.RiderId);
                    if (blockedRider != null && blockedRider.DateBlocked != null)
                        throw new RiderIsAlreadyBlockedException();

                    if (blockedRider == null)
                    {
                        var riderStatus = await context.RiderStatus.SingleOrDefaultAsync(o => o.Status.ToLower() == "blocked");
                        if (riderStatus != null)
                        {
                            rider.RiderStatusId = riderStatus.RiderStatusId;
                            context.Update(rider);
                            await context.SaveChangesAsync();

                            context.BlockedRiders.Add(entity);
                            await context.SaveChangesAsync();
                        }
                    }
                }

                var result = mapper.Map<CreateBlockedRiderDto>(entity);
                return result;
            }
        }
    }
}
