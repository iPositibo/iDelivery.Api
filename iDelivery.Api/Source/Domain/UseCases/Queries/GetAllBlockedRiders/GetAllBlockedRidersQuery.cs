using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBlockedRidersQuery : IRequest<List<GetAllBlockedRidersDto>>
    {
        private class GetAllBlockedRidersQueryHandler : IRequestHandler<GetAllBlockedRidersQuery, List<GetAllBlockedRidersDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllBlockedRidersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllBlockedRidersDto>> Handle(GetAllBlockedRidersQuery request, CancellationToken cancellationToken)
            {
                var riders = await context.BlockedRiders.ToListAsync();
                if (riders == null)
                    throw new NotFoundException();

                var result = mapper.Map<List<GetAllBlockedRidersDto>>(riders);

                foreach (var item in result)
                {
                    var rider = await context.Riders.FindAsync(item.RiderId);
                    if (rider != null)
                        item.RiderName = $"{ rider.LastName }, {rider.FirstName}";

                    item.DateBlockedFormatted = item.DateBlocked.ToString("MM/dd/yyyy");
                }

                return result;
            }
        }
    }
}
