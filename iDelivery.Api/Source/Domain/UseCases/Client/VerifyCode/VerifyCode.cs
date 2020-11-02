using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class VerifyCode : IRequest<VerifyCodeDto>
    {
        public string Code { get; }

        public VerifyCode(string code) => this.Code = code;

        private class VerifyCodeHandler : IRequestHandler<VerifyCode, VerifyCodeDto>
        {
            private DataContext context;
            private IMapper mapper;

            public VerifyCodeHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<VerifyCodeDto> Handle(VerifyCode request, CancellationToken cancellationToken)
            {
                var customer = await context.Customers.SingleOrDefaultAsync(o => o.VerificationCode == request.Code);
                if (customer == null)
                    throw new NotFoundException();

                customer.IsVerified = true;
                context.Customers.Update(customer);
                await context.SaveChangesAsync();

                return mapper.Map<VerifyCodeDto>(customer);
            }
        }
    }
}
