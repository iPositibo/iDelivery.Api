using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateAppRatingCommand : IRequest<CreateAppRatingDto>
    {
        public CreateAppRatingDto Dto { get; }

        public CreateAppRatingCommand(CreateAppRatingDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateAppRatingCommand, CreateAppRatingDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateAppRatingDto> Handle(CreateAppRatingCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<AppRating>(request.Dto);

                context.AppRatings.Add(entity);
                await context.SaveChangesAsync();

                return mapper.Map<CreateAppRatingDto>(entity);
            }
        }
    }
}
