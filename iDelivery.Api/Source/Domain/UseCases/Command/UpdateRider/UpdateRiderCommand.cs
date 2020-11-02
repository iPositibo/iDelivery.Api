using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateRiderCommand : IRequest
    {
        public int RiderId { get; set; }
        public UpdateRiderDto Dto { get; }

        public UpdateRiderCommand(int riderId, UpdateRiderDto dto)
        {
            this.RiderId = riderId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateRiderCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateRiderCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Rider>(request.Dto);

                var rider = await context.Riders.FindAsync(request.RiderId);
                if (rider == null)
                    throw new NotFoundException();

                // validate if username is already taken.
                //if (await context.Riders.AnyAsync(o => o.UserId == entity.UserId))
                //    throw new UsernameIsAlreadyTakenException();

                // update rider properties
                rider.FirstName = entity.FirstName;
                rider.LastName = entity.LastName;
                rider.PhotoUrl = entity.PhotoUrl;
                rider.Address = entity.Address;
                rider.ContactNumber = entity.ContactNumber;
                rider.RiderStatusId = entity.RiderStatusId;
                rider.UserId = entity.UserId;
                rider.ActivateEmailReceipts = entity.ActivateEmailReceipts;
                rider.IsOnline = entity.IsOnline;

                context.Update(rider);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
