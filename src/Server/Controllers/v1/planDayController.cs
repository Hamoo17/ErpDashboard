using ErpDashboard.Application.Features.PlanDays.Command.AddEdit;
using ErpDashboard.Application.Features.PlanDays.Command.Delete;
using ErpDashboard.Application.Features.PlanDays.Query.GetAll;
using ErpDashboard.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ErpDashboard.Server.Controllers.v1
{
    public class planDayController : BaseApiController<planDayController>
    {
        /// <summary>
        /// Create/Update a PlanDay
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.planday.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditPlanDaysCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a PlanDay
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.planday.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeletePlanDayCommand { ID = id }));
        }

        /// <summary>
        /// Get All PlanDay
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.planday.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var PlanDays= await _mediator.Send(new GetAllPlanDayQuery());
            return Ok(PlanDays);
        }


    }
}
