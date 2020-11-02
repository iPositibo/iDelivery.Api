using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllReportCustomersQuery : IRequest<List<GetAllReportCustomersDto>>
    {
        private class GetAllReportCustomersQueryHandler : IRequestHandler<GetAllReportCustomersQuery, List<GetAllReportCustomersDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllReportCustomersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllReportCustomersDto>> Handle(GetAllReportCustomersQuery request, CancellationToken cancellationToken)
            {
                var result = await context.ReportCustomers.ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                var reports = mapper.Map<List<GetAllReportCustomersDto>>(result);
                foreach(var report in reports)
                    report.DateReportedFormatted = report.DateReported.ToString("MM/dd/yyyy");

                return reports;
            }
        }
    }
}
