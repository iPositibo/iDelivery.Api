using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllCutomerStatusQuery : IRequest<List<GetAllCustomerStatusDto>>
    {
        private class GetAllCutomerStatusQueryHandler : IRequestHandler<GetAllCutomerStatusQuery, List<GetAllCustomerStatusDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllCutomerStatusQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllCustomerStatusDto>> Handle(GetAllCutomerStatusQuery request, CancellationToken cancellationToken)
            {
                var result = mapper.Map<List<GetAllCustomerStatusDto>>(await context.CustomerStatus.ToListAsync());
                return result;
            }
        }
    }
}
