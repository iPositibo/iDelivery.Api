using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateWalletLogCommand : IRequest<CreateWalletLogDto>
    {
        public CreateWalletLogDto Dto { get; }

        public CreateWalletLogCommand(CreateWalletLogDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateWalletLogCommand, CreateWalletLogDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateWalletLogDto> Handle(CreateWalletLogCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<WalletLog>(request.Dto);
                context.WalletLogs.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateWalletLogDto>(entity);
                return result;
            }
        }
    }
}
