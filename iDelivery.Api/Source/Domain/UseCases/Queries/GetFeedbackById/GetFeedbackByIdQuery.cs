using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetFeedbackByIdQuery : IRequest<GetFeedbackByIdDto>
    {
        public int Id { get; }

        public GetFeedbackByIdQuery(int id) => this.Id = id;

        private class GetFeedbackByIdQueryHandler : IRequestHandler<GetFeedbackByIdQuery, GetFeedbackByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetFeedbackByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetFeedbackByIdDto> Handle(GetFeedbackByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.Feedbacks.FindAsync(request.Id);
                return mapper.Map<GetFeedbackByIdDto>(user);
            }
        }
    }
}
