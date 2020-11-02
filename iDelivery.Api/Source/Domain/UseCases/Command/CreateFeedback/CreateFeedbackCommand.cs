using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateFeedbackCommand : IRequest<CreateFeedbackDto>
    {
        public CreateFeedbackDto Dto { get; }

        public CreateFeedbackCommand(CreateFeedbackDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateFeedbackCommand, CreateFeedbackDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateFeedbackDto> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Feedback>(request.Dto);
                entity.DateReported = DateTime.UtcNow;

                context.Feedbacks.Add(entity);
                await context.SaveChangesAsync();

                return mapper.Map<CreateFeedbackDto>(entity);
            }
        }
    }
}
