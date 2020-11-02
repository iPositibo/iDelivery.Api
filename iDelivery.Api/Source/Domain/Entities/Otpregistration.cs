using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class Otpregistration
    {
        public int OtpregistrationId { get; set; }
        public string Otpcode { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
