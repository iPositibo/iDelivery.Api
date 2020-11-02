using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RateCustomer : IRequest<int>
    {
        public RateCustomerDto Dto { get; }

        public RateCustomer(RateCustomerDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<RateCustomer, int>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<int> Handle(RateCustomer request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<CustomerRating>(request.Dto);
                entity.DateCreated = DateTime.UtcNow;
                context.CustomerRatings.Add(entity);
                await context.SaveChangesAsync();

                return entity.CustomerRatingId;
            }
        }
    }
}
