﻿using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBookingHistoryCommand : IRequest<int>
    {
        public CreateBookingHistoryDto Dto { get; }

        public CreateBookingHistoryCommand(CreateBookingHistoryDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateBookingHistoryCommand, int>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<int> Handle(CreateBookingHistoryCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<BookingHistory>(request.Dto);
                context.BookingHistories.Add(entity);
                await context.SaveChangesAsync();

                return entity.BookingHistoryId;
            }
        }
    }
}