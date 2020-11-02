using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetExternalAccountByIdQuery : IRequest<GetExternalAccountByIdDto>
    {
        public int Id { get; }

        public GetExternalAccountByIdQuery(int id) => this.Id = id;

        private class GetExternalAccountByIdQueryHandler : IRequestHandler<GetExternalAccountByIdQuery, GetExternalAccountByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetExternalAccountByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetExternalAccountByIdDto> Handle(GetExternalAccountByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.ExternalAccounts.FindAsync(request.Id);
                if (result == null)
                    throw new NotFoundException();

                var account = mapper.Map<GetExternalAccountByIdDto>(result);
                account.DateCreatedFormatted = account.DateCreated.ToString("MM/dd/yyyy");

                return account;
            }
        }
    }
}
