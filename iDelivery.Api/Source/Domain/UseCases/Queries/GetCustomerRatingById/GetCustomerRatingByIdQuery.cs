using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetCustomerRatingByIdQuery : IRequest<GetCustomerRatingByIdDto>
    {
        public int Id { get; }

        public GetCustomerRatingByIdQuery(int id) => this.Id = id;

        private class GetCustomerRatingByIdQueryHandler : IRequestHandler<GetCustomerRatingByIdQuery, GetCustomerRatingByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetCustomerRatingByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetCustomerRatingByIdDto> Handle(GetCustomerRatingByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.CustomerRatings.FindAsync(request.Id);
                if (result == null)
                    throw new NotFoundException();

                var user = mapper.Map<GetCustomerRatingByIdDto>(result);
                user.DateCreatedFormatted = user.DateCreated.ToString("MM/dd/yyyy");

                return user;
            }
        }
    }
}
