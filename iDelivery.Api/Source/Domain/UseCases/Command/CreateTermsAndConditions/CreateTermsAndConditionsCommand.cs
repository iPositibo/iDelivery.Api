using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateTermsAndConditionsCommand : IRequest<CreateTermsAndConditionsDto>
    {
        public CreateTermsAndConditionsDto Dto { get; }

        public CreateTermsAndConditionsCommand(CreateTermsAndConditionsDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateTermsAndConditionsCommand, CreateTermsAndConditionsDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateTermsAndConditionsDto> Handle(CreateTermsAndConditionsCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<TermsAndCondition>(request.Dto);
                context.TermsAndConditions.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateTermsAndConditionsDto>(entity);
                return result;
            }
        }
    }
}
