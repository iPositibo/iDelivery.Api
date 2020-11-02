using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateWalletLogCommand : IRequest
    {
        public int WalletLogId { get; set; }
        public UpdateWalletLogDto Dto { get; }

        public UpdateWalletLogCommand(int walletLogId, UpdateWalletLogDto dto)
        {
            this.WalletLogId = walletLogId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateWalletLogCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateWalletLogCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<WalletLog>(request.Dto);

                var walletLog = await context.WalletLogs.FindAsync(request.WalletLogId);
                if (walletLog == null)
                    throw new NotFoundException();

                // update wallet properties
                walletLog.WalletLogId = entity.WalletLogId;
                walletLog.LogDate = entity.LogDate;

                context.Update(walletLog);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
