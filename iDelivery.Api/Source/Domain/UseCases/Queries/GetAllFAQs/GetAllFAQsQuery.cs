using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries.GetAllFAQs
{
    public class GetAllFAQsQuery : IRequest<List<GetAllFAQsDto>>
    {
        private class GetAllFAQsQueryHandler : IRequestHandler<GetAllFAQsQuery, List<GetAllFAQsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllFAQsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllFAQsDto>> Handle(GetAllFAQsQuery request, CancellationToken cancellationToken)
            {
                var result = await context.Faqs.ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                return mapper.Map<List<GetAllFAQsDto>>(result);
            }
        }
    }
}
