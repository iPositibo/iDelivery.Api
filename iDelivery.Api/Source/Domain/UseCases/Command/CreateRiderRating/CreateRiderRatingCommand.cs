using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateRiderRatingCommand : IRequest<CreateRiderRatingDto>
    {
        public CreateRiderRatingDto Dto { get; }

        public CreateRiderRatingCommand(CreateRiderRatingDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateRiderRatingCommand, CreateRiderRatingDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateRiderRatingDto> Handle(CreateRiderRatingCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<RiderRating>(request.Dto);

                entity.DateCreated = DateTime.UtcNow;
                context.RiderRatings.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateRiderRatingDto>(entity);
                return result;
            }
        }
    }
}
