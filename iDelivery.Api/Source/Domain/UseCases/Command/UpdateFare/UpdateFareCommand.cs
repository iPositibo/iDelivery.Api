using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateFareCommand : IRequest
    {
        public int FareId { get; set; }
        public UpdateFareDto Dto { get; }

        public UpdateFareCommand(int fareId, UpdateFareDto dto)
        {
            this.FareId = fareId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateFareCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateFareCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Fare>(request.Dto);

                var fare = await context.Fares.FindAsync(request.FareId);
                if (fare == null)
                    throw new NotFoundException();

                // update fare properties
                fare.BaseFare = entity.BaseFare;
                fare.CompanyPercentage = entity.CompanyPercentage;
                fare.PricePerKilometer = entity.PricePerKilometer;
                fare.RidersPercentage = entity.RidersPercentage;
                fare.Surcharge = entity.Surcharge;
                fare.TotalBaseKilometers = entity.TotalBaseKilometers;
                fare.IsDefault = entity.IsDefault;
                fare.AllowedBalance = entity.AllowedBalance;

                context.Update(fare);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
