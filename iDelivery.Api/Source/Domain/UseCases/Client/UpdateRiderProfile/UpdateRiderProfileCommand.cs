using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class UpdateRiderProfileCommand : IRequest<UpdateRiderProfileDto>
    {
        public UpdateRiderProfileDto Dto { get; }

        public UpdateRiderProfileCommand(UpdateRiderProfileDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<UpdateRiderProfileCommand, UpdateRiderProfileDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<UpdateRiderProfileDto> Handle(UpdateRiderProfileCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Rider>(request.Dto);

                var rider = await context.Riders.SingleOrDefaultAsync(o => o.UserId == entity.UserId);
                if (rider == null)
                    throw new NotFoundException();

                // update rider properties
                rider.FirstName = entity.FirstName;
                rider.LastName = entity.LastName;
                rider.PhotoUrl = entity.PhotoUrl;
                rider.Address = entity.Address;
                rider.Email = entity.Email;
                rider.ContactNumber = entity.ContactNumber;

                context.Update(rider);
                context.SaveChanges();

                return mapper.Map<UpdateRiderProfileDto>(rider);
            }
        }
    }
}
