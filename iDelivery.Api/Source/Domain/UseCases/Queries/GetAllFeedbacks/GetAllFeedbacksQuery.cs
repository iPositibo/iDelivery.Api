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
    public class GetAllFeedbacksQuery : IRequest<List<GetAllFeedbacksDto>>
    {
        private class GetAllFeedbacksQueryHandler : IRequestHandler<GetAllFeedbacksQuery, List<GetAllFeedbacksDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllFeedbacksQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllFeedbacksDto>> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
            {
                var feedbacks = await context.Feedbacks.ToListAsync();
                if (feedbacks == null)
                    throw new NotFoundException();

                var result = mapper.Map<List<GetAllFeedbacksDto>>(feedbacks);
                foreach(var item in result)
                {
                    var name = default(string);
                    var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == item.UserId);
                    if (customer != null)
                        name = $"{ customer.LastName }, {customer.FirstName}";

                    var rider = await context.Riders.SingleOrDefaultAsync(o => o.RiderId == item.UserId);
                    if(rider != null)
                        name = $"{ rider.LastName }, {rider.FirstName}";

                    item.Username = name;

                    item.DateReportedFormatted = item.DateReported.ToString("MM/dd/yyyy");
                }

                return result;
            }
        }
    }
}
