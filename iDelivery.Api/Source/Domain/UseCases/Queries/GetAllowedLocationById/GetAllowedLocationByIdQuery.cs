using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllowedLocationByIdQuery : IRequest<GetAllowedLocationByIdDto>
    {
        public int Id { get; }

        public GetAllowedLocationByIdQuery(int id) => this.Id = id;

        private class GetAllowedLocationByIdQueryHandler : IRequestHandler<GetAllowedLocationByIdQuery, GetAllowedLocationByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetAllowedLocationByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetAllowedLocationByIdDto> Handle(GetAllowedLocationByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.AllowedLocations.FindAsync(request.Id);
                if (result == null)
                    throw new NotFoundException();

                var allowedLocation = mapper.Map<GetAllowedLocationByIdDto>(result);

                return allowedLocation;
            }
        }
    }
}
