using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateFAQCommand : IRequest<CreateFAQDto>
    {
        public CreateFAQDto Dto { get; }

        public CreateFAQCommand(CreateFAQDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateFAQCommand, CreateFAQDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateFAQDto> Handle(CreateFAQCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Faq>(request.Dto);
                context.Faqs.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateFAQDto>(entity);
                return result;
            }
        }
    }
}
