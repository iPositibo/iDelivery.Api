using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetMenuAccessByIdQuery : IRequest<GetMenuAccessByIdDto>
    {
        public int Id { get; }

        public GetMenuAccessByIdQuery(int id) => this.Id = id;

        private class GetMenuAccessByIdQueryHandler : IRequestHandler<GetMenuAccessByIdQuery, GetMenuAccessByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetMenuAccessByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetMenuAccessByIdDto> Handle(GetMenuAccessByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.MenuAccesses.FindAsync(request.Id);
                return mapper.Map<GetMenuAccessByIdDto>(user);
            }
        }
    }
}
