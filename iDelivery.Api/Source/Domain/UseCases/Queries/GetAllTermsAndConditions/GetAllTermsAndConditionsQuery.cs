using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllTermsAndConditionsQuery : IRequest<List<GetAllTermsAndConditionsDto>>
    {
        private class GetAllTermsAndConditionsQueryHandler : IRequestHandler<GetAllTermsAndConditionsQuery, List<GetAllTermsAndConditionsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllTermsAndConditionsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllTermsAndConditionsDto>> Handle(GetAllTermsAndConditionsQuery request, CancellationToken cancellationToken)
            {
                var result = mapper.Map<List<GetAllTermsAndConditionsDto>>(await context.TermsAndConditions.ToListAsync());
                return result;
            }
        }
    }
}
