using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateFareCommand : IRequest<CreateFareDto>
    {
        public CreateFareDto Dto { get; }

        public CreateFareCommand(CreateFareDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateFareCommand, CreateFareDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateFareDto> Handle(CreateFareCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Fare>(request.Dto);
                if (entity.IsDefault.GetValueOrDefault())
                {
                    if (await context.Fares.AnyAsync(o => o.IsDefault == true))
                        throw new DefaultFareAlreadyExistException();
                }

                context.Fares.Add(entity);
                await context.SaveChangesAsync();

                return mapper.Map<CreateFareDto>(entity);
            }
        }
    }
}
