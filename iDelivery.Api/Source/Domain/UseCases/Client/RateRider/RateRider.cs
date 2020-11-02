using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RateRider : IRequest<int>
    {
        public RateRiderDto Dto { get; }

        public RateRider(RateRiderDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<RateRider, int>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<int> Handle(RateRider request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<RiderRating>(request.Dto);
                entity.DateCreated = DateTime.UtcNow;
                context.RiderRatings.Add(entity);
                await context.SaveChangesAsync();

                return entity.RiderRatingId;
            }
        }
    }
}
