using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllFaresQuery : IRequest<List<GetAllFaresDto>>
    {
        private class GetAllFaresQueryHandler : IRequestHandler<GetAllFaresQuery, List<GetAllFaresDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllFaresQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllFaresDto>> Handle(GetAllFaresQuery request, CancellationToken cancellationToken)
            {
                var result = mapper.Map<List<GetAllFaresDto>>(await context.Fares.ToListAsync());
                return result;
            }
        }
    }
}
