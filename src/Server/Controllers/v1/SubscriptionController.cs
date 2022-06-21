
using ErpDashboard.Application.Features.Subscriptions.Queries.GetSidByPhone;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ErpDashboard.Server.Controllers.v1
{
  
    public class SubscriptionController : BaseApiController<SubscriptionController>
    {
        /// <summary>
        /// Get All Related Subscription By Phone Number
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>Status 200 OK</returns>
        [HttpGet("{phone}")]
        public async Task<IActionResult> GetAll(string phone)
        {
            var PlanCategories = await _mediator.Send(new GetSidByPhoneQury() { PhoneNumber = phone});
            return Ok(PlanCategories);
        }
    }
}
