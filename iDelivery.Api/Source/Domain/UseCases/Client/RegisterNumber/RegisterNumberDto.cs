using System;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RegisterNumberDto
    {
        public string Otpcode { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
