using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class Faq
    {
        public int Faqid { get; set; }
        public string Faqcontent { get; set; }
        public string Answer { get; set; }
    }
}
