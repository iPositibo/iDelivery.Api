using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateCustomerStatusCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateCustomerStatusDto Dto { get; }

        public UpdateCustomerStatusCommand(int id, UpdateCustomerStatusDto dto)
        {
            this.Id = id;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateCustomerStatusCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateCustomerStatusCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<CustomerStatus>(request.Dto);

                var customerStatus = await context.CustomerStatus.FindAsync(request.Id);
                if (customerStatus == null)
                    throw new NotFoundException();

                // update customer status properties
                customerStatus.Status = entity.Status;

                context.Update(customerStatus);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
