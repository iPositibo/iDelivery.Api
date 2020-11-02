using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllWalletLogsQuery : IRequest<List<GetAllWalletLogsDto>>
    {
        private class GetAllWalletLogsQueryHandler : IRequestHandler<GetAllWalletLogsQuery, List<GetAllWalletLogsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllWalletLogsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllWalletLogsDto>> Handle(GetAllWalletLogsQuery request, CancellationToken cancellationToken)
            {
                var logs = mapper.Map<List<GetAllWalletLogsDto>>(await context.WalletLogs.ToListAsync());
                foreach (var log in logs)
                {
                    log.LogDateFormatted = log.LogDate.ToString("MM/dd/yyyy");
                    var rider = await context.Riders.FindAsync(log.RiderId);
                    if(rider != null)
                        log.RiderName = $"{ rider.LastName }, { rider.FirstName }";
                }

                return logs;
            }
        }
    }
}
