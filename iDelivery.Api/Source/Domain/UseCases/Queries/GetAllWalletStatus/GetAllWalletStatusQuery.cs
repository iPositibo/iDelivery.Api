using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllWalletStatusQuery : IRequest<List<GetAllWalletStatusDto>>
    {
        private class GetAllWalletStatusQueryHandler : IRequestHandler<GetAllWalletStatusQuery, List<GetAllWalletStatusDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllWalletStatusQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllWalletStatusDto>> Handle(GetAllWalletStatusQuery request, CancellationToken cancellationToken)
            {
                var result = mapper.Map<List<GetAllWalletStatusDto>>(await context.WalletStatus.ToListAsync());
                return result;
            }
        }
    }
}
