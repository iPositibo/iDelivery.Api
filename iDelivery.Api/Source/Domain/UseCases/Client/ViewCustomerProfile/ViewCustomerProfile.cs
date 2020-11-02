using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewCustomerProfile : IRequest<ViewCustomerProfileDto>
    {
        public int Id { get; }

        public ViewCustomerProfile(int id) => this.Id = id;

        private class ViewCustomerProfileHandler : IRequestHandler<ViewCustomerProfile, ViewCustomerProfileDto>
        {
            private DataContext context;
            private IMapper mapper;

            public ViewCustomerProfileHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<ViewCustomerProfileDto> Handle(ViewCustomerProfile request, CancellationToken cancellationToken)
            {
                var user = await context.Customers.FindAsync(request.Id);
                return mapper.Map<ViewCustomerProfileDto>(user);
            }
        }
    }
}
