﻿using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBlockedRiderMapper : Profile
    {
        public CreateBlockedRiderMapper()
        {
            CreateMap<BlockedRider, CreateBlockedRiderDto>();
            CreateMap<CreateBlockedRiderDto, BlockedRider>();
        }
    }
}
