using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewOnlineRidersQuery : IRequest<ViewOnlineRidersDto>
    {
        public int Id { get; }

        public ViewOnlineRidersQuery(int id) => this.Id = id;

        private class ViewOnlineRidersQueryHandler : IRequestHandler<ViewOnlineRidersQuery, ViewOnlineRidersDto>
        {
            private DataContext context;
            private IMapper mapper;

            public ViewOnlineRidersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<ViewOnlineRidersDto> Handle(ViewOnlineRidersQuery request, CancellationToken cancellationToken)
            {
                var user = await context.Riders.Where(o => o.IsOnline).ToListAsync();
                return mapper.Map<ViewOnlineRidersDto>(user);
            }
        }
    }
}
