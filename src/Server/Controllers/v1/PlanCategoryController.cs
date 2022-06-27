using ErpDashboard.Application.Features.PlanCategory.Command.AddEdit;
using ErpDashboard.Application.Features.PlanCategory.Command.Delete;
using ErpDashboard.Application.Features.PlanCategory.Query.GetAll;
using ErpDashboard.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpDashboard.Server.Controllers.v1
{

    public class PlanCategoryController : BaseApiController<PlanCategoryController>
    {
        /// <summary>
        /// Create/Update a Plan Category
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.PlanCategory.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditPlanCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a PlanCategory
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.PlanCategory.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeletePlanCategoryCommand { ID = id }));
        }

        /// <summary>
        /// Get All Plancategory
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.PlanCategory.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var PlanCategories = await _mediator.Send(new GetAllPlanCategoryQuery());
            return Ok(PlanCategories);
        }

    }
}
