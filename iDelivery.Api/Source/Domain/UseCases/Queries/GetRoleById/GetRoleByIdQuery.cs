using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetRoleByIdQuery : IRequest<GetRoleByIdDto>
    {
        public int Id { get; }

        public GetRoleByIdQuery(int id) => this.Id = id;

        private class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, GetRoleByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetRoleByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetRoleByIdDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.Roles.FindAsync(request.Id);
                return mapper.Map<GetRoleByIdDto>(user);
            }
        }
    }
}
