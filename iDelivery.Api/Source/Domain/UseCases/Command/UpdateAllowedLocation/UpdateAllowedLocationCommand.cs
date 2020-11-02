using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateAllowedLocationCommand : IRequest
    {
        public int AllowedLocationId { get; set; }
        public UpdateAllowedLocationDto Dto { get; }

        public UpdateAllowedLocationCommand(int allowedLocationId, UpdateAllowedLocationDto dto)
        {
            this.AllowedLocationId = allowedLocationId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateAllowedLocationCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateAllowedLocationCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<AllowedLocation>(request.Dto);

                var allowedLocation = await context.AllowedLocations.FindAsync(request.AllowedLocationId);
                if (allowedLocation == null)
                    throw new NotFoundException();

                // update allowed location properties
                allowedLocation.Location = entity.Location;
                allowedLocation.IsAllowed = entity.IsAllowed;
               
                context.Update(allowedLocation);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
