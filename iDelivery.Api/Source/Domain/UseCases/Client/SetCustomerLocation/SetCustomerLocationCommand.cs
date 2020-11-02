using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class SetCustomerLocationCommand : IRequest
    {
        public int CustomerId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public SetCustomerLocationCommand(int customerId, string longitude, string latitude)
        {
            this.CustomerId = customerId;
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        private class RequestHandler : IRequestHandler<SetCustomerLocationCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(SetCustomerLocationCommand request, CancellationToken cancellationToken)
            {
                var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == request.CustomerId);
                if (customer == null)
                    throw new NotFoundException();

                // update customer properties
                customer.Longitude = request.Longitude;
                customer.Latitude = request.Latitude;

                context.Update(customer);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
