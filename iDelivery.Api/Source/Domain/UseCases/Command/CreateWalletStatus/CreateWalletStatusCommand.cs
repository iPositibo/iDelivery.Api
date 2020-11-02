using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateWalletStatusCommand : IRequest<CreateWalletStatusDto>
    {
        public CreateWalletStatusDto Dto { get; }

        public CreateWalletStatusCommand(CreateWalletStatusDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateWalletStatusCommand, CreateWalletStatusDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateWalletStatusDto> Handle(CreateWalletStatusCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<WalletStatus>(request.Dto);
                context.WalletStatus.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateWalletStatusDto>(entity);
                return result;
            }
        }
    }
}
