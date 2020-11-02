using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateCustomerCommand : IRequest
    {
        public int CustomerId { get; set; }
        public UpdateCustomerDto Dto { get; }

        public UpdateCustomerCommand(int customerId, UpdateCustomerDto dto)
        {
            this.CustomerId = customerId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateCustomerCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Customer>(request.Dto);

                var customer = await context.Customers.FindAsync(request.CustomerId);
                if (customer == null)
                    throw new NotFoundException();
                
                // validate if username is already taken.
                //if (await context.Customers.AnyAsync(o => o.UserId == entity.UserId))
                //    throw new UsernameIsAlreadyTakenException();

                // update customer properties
                customer.FirstName = entity.FirstName;
                customer.LastName = entity.LastName;
                customer.PhotoUrl = entity.PhotoUrl;
                customer.Address = entity.Address;
                customer.ContactNumber = entity.ContactNumber;
                customer.ActivateEmailReceipts = entity.ActivateEmailReceipts;
                customer.CustomerStatusId = entity.CustomerStatusId;
                customer.FareId = entity.FareId;
                customer.UserId = entity.UserId;

                context.Update(customer);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
