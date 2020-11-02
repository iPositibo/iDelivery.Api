using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RateApp : IRequest<RateAppDto>
    {
        public RateAppDto Dto { get; }

        public RateApp(RateAppDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<RateApp, RateAppDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<RateAppDto> Handle(RateApp request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<AppRating>(request.Dto);
                entity.DateCreated = DateTime.UtcNow;

                context.AppRatings.Add(entity);
                await context.SaveChangesAsync();

                return mapper.Map<RateAppDto>(entity);
            }
        }
    }
}
