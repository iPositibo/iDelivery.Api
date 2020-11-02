using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateMenuAccessCommand : IRequest<CreateMenuAccessDto>
    {
        public CreateMenuAccessDto Dto { get; }

        public CreateMenuAccessCommand(CreateMenuAccessDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateMenuAccessCommand, CreateMenuAccessDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateMenuAccessDto> Handle(CreateMenuAccessCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<MenuAccess>(request.Dto);

                context.MenuAccesses.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateMenuAccessDto>(entity);
                return result;
            }
        }
    }
}
