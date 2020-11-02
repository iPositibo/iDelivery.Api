using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBookingStatusQuery : IRequest<List<GetAllBookingStatusDto>>
    {
        private class GetAllBookingStatusQueryHandler : IRequestHandler<GetAllBookingStatusQuery, List<GetAllBookingStatusDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllBookingStatusQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllBookingStatusDto>> Handle(GetAllBookingStatusQuery request, CancellationToken cancellationToken)
            {
                var result = mapper.Map<List<GetAllBookingStatusDto>>(await context.BookingStatus.ToListAsync());
                return result;
            }
        }
    }
}
