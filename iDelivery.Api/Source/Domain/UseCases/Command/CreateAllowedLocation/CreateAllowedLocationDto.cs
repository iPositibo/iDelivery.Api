﻿namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateAllowedLocationDto
    {
        public string Location { get; set; }
        public bool IsAllowed { get; set; }
    }
}
