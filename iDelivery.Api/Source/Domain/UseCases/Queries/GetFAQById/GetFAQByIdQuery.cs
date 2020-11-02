using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetFAQByIdQuery : IRequest<GetFAQByIdDto>
    {
        public int Id { get; }

        public GetFAQByIdQuery(int id) => this.Id = id;

        private class GetFAQByIdQueryHandler : IRequestHandler<GetFAQByIdQuery, GetFAQByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetFAQByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetFAQByIdDto> Handle(GetFAQByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.Faqs.FindAsync(request.Id);
                return mapper.Map<GetFAQByIdDto>(user);
            }
        }
    }
}
