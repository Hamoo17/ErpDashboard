using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Subscriptions.Queries.Dto
{
    public class CustomerInfoDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<SubscriptionDetailDto> Subscriptions { get; set; }
    }
}
