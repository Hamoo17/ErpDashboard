using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ErpDashboard.Application.Enums.ErpSystemEnums;

namespace ErpDashboard.Application.Features.Subscriptions.Queries.Dto
{
    public class SubscriptionDetailDto
    {
        public int SubscriptionId { get; set; }
        public string DeliveryAddres { get; set; }
        public string PlanName { get; set; }
        public string PlanTitle { get; set; }
        public DateTime StartDate { get; set; }
        public string DriverName { get; set; }
        public string BranchName { get; set; }
        public int RemainingDays { get; set; }
        public int Duration { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
