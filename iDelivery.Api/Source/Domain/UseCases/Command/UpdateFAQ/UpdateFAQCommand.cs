using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateFAQCommand : IRequest
    {
        public int FaqId { get; set; }
        public UpdateFAQDto Dto { get; }

        public UpdateFAQCommand(int faqId, UpdateFAQDto dto)
        {
            this.FaqId = faqId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateFAQCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateFAQCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Faq>(request.Dto);

                var faq = await context.Faqs.FindAsync(request.FaqId);
                if (faq == null)
                    throw new NotFoundException();

                // update faq properties
                faq.Faqcontent = entity.Faqcontent;
                faq.Answer = entity.Answer;
              
                context.Update(faq);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
