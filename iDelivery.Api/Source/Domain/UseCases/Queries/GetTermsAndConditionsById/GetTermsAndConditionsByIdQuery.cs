using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetTermsAndConditionsByIdQuery : IRequest<GetTermsAndConditionsByIdDto>
    {
        public int Id { get; }

        public GetTermsAndConditionsByIdQuery(int id) => this.Id = id;

        private class GetTermsAndConditionsByIdQueryHandler : IRequestHandler<GetTermsAndConditionsByIdQuery, GetTermsAndConditionsByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetTermsAndConditionsByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetTermsAndConditionsByIdDto> Handle(GetTermsAndConditionsByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.TermsAndConditions.FindAsync(request.Id);
                return mapper.Map<GetTermsAndConditionsByIdDto>(user);
            }
        }
    }
}
