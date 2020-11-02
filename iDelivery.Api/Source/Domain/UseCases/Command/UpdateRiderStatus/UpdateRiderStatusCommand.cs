using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateRiderStatusCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateRiderStatusDto Dto { get; }

        public UpdateRiderStatusCommand(int id, UpdateRiderStatusDto dto)
        {
            this.Id = id;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateRiderStatusCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateRiderStatusCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<RiderStatus>(request.Dto);

                var riderStatus = await context.RiderStatus.FindAsync(request.Id);
                if (riderStatus == null)
                    throw new NotFoundException();

                // update rider status properties
                riderStatus.Status = entity.Status;

                context.Update(entity);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
