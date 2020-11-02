using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateWalletStatusCommand : IRequest
    {
        public int WalletStatusId { get; set; }
        public UpdateWalletStatusDto Dto { get; }

        public UpdateWalletStatusCommand(int walletStatusId, UpdateWalletStatusDto dto)
        {
            this.WalletStatusId = walletStatusId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateWalletStatusCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateWalletStatusCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<WalletStatus>(request.Dto);

                var wallet = await context.WalletStatus.FindAsync(request.WalletStatusId);
                if (wallet == null)
                    throw new NotFoundException();

                // update wallet status properties
                wallet.Status = entity.Status;

                context.Update(wallet);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
