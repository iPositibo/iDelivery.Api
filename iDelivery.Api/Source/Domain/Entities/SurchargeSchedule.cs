using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class SurchargeSchedule
    {
        public int SurchargeScheduleId { get; set; }
        public DateTime ScheduleRun { get; set; }
        public int TimeLimit { get; set; }
    }
}
