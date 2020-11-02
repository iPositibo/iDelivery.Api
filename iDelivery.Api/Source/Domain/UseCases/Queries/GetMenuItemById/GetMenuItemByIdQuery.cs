using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetMenuItemByIdQuery : IRequest<GetMenuItemByIdDto>
    {
        public int Id { get; }

        public GetMenuItemByIdQuery(int id) => this.Id = id;

        private class GetMenuItemByIdQueryHandler : IRequestHandler<GetMenuItemByIdQuery, GetMenuItemByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetMenuItemByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetMenuItemByIdDto> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.MenuItems.FindAsync(request.Id);
                return mapper.Map<GetMenuItemByIdDto>(user);
            }
        }
    }
}
