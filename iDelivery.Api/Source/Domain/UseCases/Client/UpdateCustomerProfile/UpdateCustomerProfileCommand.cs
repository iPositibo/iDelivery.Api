using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class UpdateCustomerProfileCommand : IRequest<UpdateCustomerProfileDto>
    {
        public UpdateCustomerProfileDto Dto { get; }

        public UpdateCustomerProfileCommand(UpdateCustomerProfileDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<UpdateCustomerProfileCommand, UpdateCustomerProfileDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<UpdateCustomerProfileDto> Handle(UpdateCustomerProfileCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Customer>(request.Dto);

                var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == entity.UserId);
                if (customer == null)
                    throw new NotFoundException();

                // update customer properties
                customer.FirstName = entity.FirstName;
                customer.LastName = entity.LastName;
                customer.PhotoUrl = entity.PhotoUrl;
                customer.Address = entity.Address;
                customer.Email = entity.Email;
                customer.ContactNumber = entity.ContactNumber;

                context.Update(customer);
                context.SaveChanges();

                return mapper.Map<UpdateCustomerProfileDto>(customer);
            }
        }
    }
}
