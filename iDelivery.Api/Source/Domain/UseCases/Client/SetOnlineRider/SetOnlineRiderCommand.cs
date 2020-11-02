using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class SetOnlineRiderCommand : IRequest<SetOnlineRiderDto>
    {
        public int RiderId { get; set; }
        public bool IsOnline { get; set; }

        public SetOnlineRiderCommand(int riderId, bool isOnline)
        {
            this.RiderId = riderId;
            this.IsOnline = isOnline;
        }

        private class RequestHandler : IRequestHandler<SetOnlineRiderCommand, SetOnlineRiderDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<SetOnlineRiderDto> Handle(SetOnlineRiderCommand request, CancellationToken cancellationToken)
            {
                var rider = await context.Riders.SingleOrDefaultAsync(o => o.UserId == request.RiderId);
                if (rider == null)
                    throw new NotFoundException();

                rider.IsOnline = request.IsOnline;
                context.Update(rider);
                context.SaveChanges();

                return mapper.Map<SetOnlineRiderDto>(rider);
            }
        }
    }
}
