﻿using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateTermsAndConditionsMapper : Profile
    {
        public CreateTermsAndConditionsMapper()
        {
            CreateMap<TermsAndCondition, CreateTermsAndConditionsDto>();
            CreateMap<CreateTermsAndConditionsDto, TermsAndCondition>();
        }
    }
}