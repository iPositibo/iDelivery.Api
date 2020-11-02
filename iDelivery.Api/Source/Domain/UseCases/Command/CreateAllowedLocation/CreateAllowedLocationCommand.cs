using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateAllowedLocationCommand : IRequest<CreateAllowedLocationDto>
    {
        public CreateAllowedLocationDto Dto { get; }

        public CreateAllowedLocationCommand(CreateAllowedLocationDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateAllowedLocationCommand, CreateAllowedLocationDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateAllowedLocationDto> Handle(CreateAllowedLocationCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<AllowedLocation>(request.Dto);

                context.AllowedLocations.Add(entity);
                await context.SaveChangesAsync();

                return mapper.Map<CreateAllowedLocationDto>(entity);
            }
        }
    }
}
