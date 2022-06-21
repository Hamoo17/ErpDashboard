
using ErpDashboard.Application.Features.Subscriptions.Queries.GetSidByPhone;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ErpDashboard.Server.Controllers.v1
{
  
    public class SubscriptionController : BaseApiController<SubscriptionController>
    {
        [HttpGet("{phone}")]
        public async Task<IActionResult> GetAll(string phone)
        {
            var PlanCategories = await _mediator.Send(new GetSidByPhoneQury() { PhoneNumber = phone});
            return Ok(PlanCategories);
        }
    }
}
