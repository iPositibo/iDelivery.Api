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
    public class GetAllExternalAccountsQuery : IRequest<List<GetAllExternalAccountsDto>>
    {
        private class GetAllExternalAccountsQueryHandler : IRequestHandler<GetAllExternalAccountsQuery, List<GetAllExternalAccountsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllExternalAccountsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllExternalAccountsDto>> Handle(GetAllExternalAccountsQuery request, CancellationToken cancellationToken)
            {
                var result = await context.ExternalAccounts.ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                return mapper.Map<List<GetAllExternalAccountsDto>>(result);
            }
        }
    }
}
