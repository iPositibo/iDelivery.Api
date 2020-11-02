using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewRiderProfile : IRequest<ViewRiderProfileDto>
    {
        public int Id { get; }

        public ViewRiderProfile(int id) => this.Id = id;

        private class ViewRiderProfileHandler : IRequestHandler<ViewRiderProfile, ViewRiderProfileDto>
        {
            private DataContext context;
            private IMapper mapper;

            public ViewRiderProfileHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<ViewRiderProfileDto> Handle(ViewRiderProfile request, CancellationToken cancellationToken)
            {
                var user = await context.Riders.FindAsync(request.Id);
                return mapper.Map<ViewRiderProfileDto>(user);
            }
        }
    }
}
