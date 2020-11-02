using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetRiderRatingByIdQuery : IRequest<GetRiderRatingByIdDto>
    {
        public int Id { get; }

        public GetRiderRatingByIdQuery(int id) => this.Id = id;

        private class GetRiderRatingByIdQueryHandler : IRequestHandler<GetRiderRatingByIdQuery, GetRiderRatingByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetRiderRatingByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetRiderRatingByIdDto> Handle(GetRiderRatingByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.RiderRatings.FindAsync(request.Id);
                return mapper.Map<GetRiderRatingByIdDto>(user);
            }
        }
    }
}
