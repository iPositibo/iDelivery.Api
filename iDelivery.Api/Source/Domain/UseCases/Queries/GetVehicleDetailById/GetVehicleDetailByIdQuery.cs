using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetVehicleDetailByIdQuery : IRequest<GetVehicleDetailByIdDto>
    {
        public int Id { get; }

        public GetVehicleDetailByIdQuery(int id) => this.Id = id;

        private class GetWalletByIdQueryHandler : IRequestHandler<GetVehicleDetailByIdQuery, GetVehicleDetailByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetWalletByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetVehicleDetailByIdDto> Handle(GetVehicleDetailByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.VehicleDetails.FindAsync(request.Id);
                if (result == null)
                    throw new NotFoundException();

                return mapper.Map<GetVehicleDetailByIdDto>(result); ;
            }
        }
    }
}
