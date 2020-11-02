using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateMenuItemCommand : IRequest<CreateMenuItemDto>
    {
        public CreateMenuItemDto Dto { get; }

        public CreateMenuItemCommand(CreateMenuItemDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateMenuItemCommand, CreateMenuItemDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateMenuItemDto> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<MenuItem>(request.Dto);

                context.MenuItems.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateMenuItemDto>(entity);
                return result;
            }
        }
    }
}
