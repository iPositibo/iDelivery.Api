using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAppRatingByIdQuery : IRequest<GetAppRatingByIdDto>
    {
        public int Id { get; }

        public GetAppRatingByIdQuery(int id) => this.Id = id;

        private class GetAppRatingByIdQueryHandler : IRequestHandler<GetAppRatingByIdQuery, GetAppRatingByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetAppRatingByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetAppRatingByIdDto> Handle(GetAppRatingByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.AppRatings.FindAsync(request.Id);
                if (result == null)
                    throw new NotFoundException();

                var rating = mapper.Map<GetAppRatingByIdDto>(result);

                var customer = await context.Customers.FindAsync(rating.CustomerId);
                if (customer != null)
                    rating.CustomerName = $"{ customer.LastName }, {customer.FirstName}";

                rating.DateCreatedFormatted = rating.DateCreated.ToString("MM/dd/yyyy");

                return rating;
            }
        }
    }
}
