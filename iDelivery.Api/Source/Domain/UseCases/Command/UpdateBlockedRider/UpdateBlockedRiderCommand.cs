using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBlockedRiderCommand : IRequest
    {
        public int RiderId { get; set; }
        public UpdateBlockedRiderDto Dto { get; }

        public UpdateBlockedRiderCommand(int riderId, UpdateBlockedRiderDto dto)
        {
            this.RiderId = riderId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateBlockedRiderCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateBlockedRiderCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<BlockedRider>(request.Dto);

                var blockedRider = await context.BlockedRiders.FindAsync(request.RiderId);
                if (blockedRider == null)
                    throw new NotFoundException();

                // update blocked rider properties
                blockedRider.RiderId = entity.RiderId;
                blockedRider.DateBlocked = DateTime.UtcNow;
                blockedRider.Reason = entity.Reason;

                context.Update(blockedRider);
                context.SaveChanges();

                // update rider
                var rider = await context.Riders.FindAsync(entity.RiderId);
                if (rider != null)
                {
                    var status = await context.RiderStatus.SingleOrDefaultAsync(o => o.Status == "Blocked");
                    if (status != null)
                        rider.RiderStatusId = status.RiderStatusId;
                }

                context.Update(rider);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
