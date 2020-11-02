using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ReceivedEmailReceipts : IRequest<ReceivedEmailReceiptsDto>
    {
        public int Id { get; set; }
        public bool IsActivateEmailReceipts { get; set; }

        public ReceivedEmailReceipts(int id, bool isActivateEmailReceipts)
        {
            this.Id = id;
            this.IsActivateEmailReceipts = isActivateEmailReceipts;
        }

        private class RequestHandler : IRequestHandler<ReceivedEmailReceipts, ReceivedEmailReceiptsDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<ReceivedEmailReceiptsDto> Handle(ReceivedEmailReceipts request, CancellationToken cancellationToken)
            {
                var receipt = new ReceivedEmailReceiptsDto();
                var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == request.Id);
                if (customer != null)
                {
                    customer.ActivateEmailReceipts = request.IsActivateEmailReceipts;
                    context.Update(customer);
                    context.SaveChanges();

                    receipt = mapper.Map<ReceivedEmailReceiptsDto>(customer);
                }
                else
                {
                    var rider = await context.Riders.SingleOrDefaultAsync(o => o.UserId == request.Id);
                    if(rider != null)
                    {
                        rider.ActivateEmailReceipts = request.IsActivateEmailReceipts;
                        context.Update(rider);
                        context.SaveChanges();

                        receipt = mapper.Map<ReceivedEmailReceiptsDto>(rider);
                    }
                }

                return receipt;
            }
        }
    }
}
