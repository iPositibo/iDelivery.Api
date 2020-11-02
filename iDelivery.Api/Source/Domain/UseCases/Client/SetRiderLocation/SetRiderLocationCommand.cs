using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class SetRiderLocationCommand : IRequest
    {
        public int RiderId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public SetRiderLocationCommand(int riderId, string longitude, string latitude)
        {
            this.RiderId = riderId;
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        private class RequestHandler : IRequestHandler<SetRiderLocationCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(SetRiderLocationCommand request, CancellationToken cancellationToken)
            {
                var rider = await context.Riders.SingleOrDefaultAsync(o => o.UserId == request.RiderId);
                if (rider == null)
                    throw new NotFoundException();

                // update rider properties
                rider.Longitude = request.Longitude;
                rider.Latitude = request.Latitude;

                context.Update(rider);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
