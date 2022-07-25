using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Interfaces.Services
{
    public interface ISubscriptionService
    {
        public TbSubscrbtionHeader ExtendSubscription(int Days);

    }
}
