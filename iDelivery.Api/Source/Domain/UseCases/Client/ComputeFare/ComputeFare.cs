using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Infrastructure.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ComputeFare : IRequest<string>
    {
        public decimal TotalKilometers { get; }

        public ComputeFare(decimal totalKilometers) => this.TotalKilometers = totalKilometers;

        private class RequestHandler : IRequestHandler<ComputeFare, string>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<string> Handle(ComputeFare request, CancellationToken cancellationToken)
            {
                var fare = await context.Fares.SingleOrDefaultAsync(o => o.IsDefault == true);
                if (fare == null)
                    throw new NotFoundException();

                return FareHelper.ComputeRiderFare(fare.PricePerKilometer, fare.BaseFare, fare.Surcharge, request.TotalKilometers);
            }
        }
    }
}
