using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateCustomerRatingCommand : IRequest<CreateCustomerRatingDto>
    {
        public CreateCustomerRatingDto Dto { get; }

        public CreateCustomerRatingCommand(CreateCustomerRatingDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateCustomerRatingCommand, CreateCustomerRatingDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateCustomerRatingDto> Handle(CreateCustomerRatingCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<CustomerRating>(request.Dto);

                entity.DateCreated = DateTime.UtcNow;
                context.CustomerRatings.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateCustomerRatingDto>(entity);
                return result;
            }
        }
    }
}
