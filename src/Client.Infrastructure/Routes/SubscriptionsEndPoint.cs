using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Routes
{
    public static class SubscriptionsEndPoint
    {
        public static string GetSidByPhone(string Phone) => $"api/v1/Subscription/{Phone}";
    }
}
