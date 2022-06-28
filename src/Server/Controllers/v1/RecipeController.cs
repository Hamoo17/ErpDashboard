using ErpDashboard.Application.Features.Recipe.Commabds.AddEdit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErpDashboard.Server.Controllers.v1
{
  
    public class RecipeController : BaseApiController<RecipeController>
    {
        /// <summary>
        /// Create/Update Item Recipe
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        // [Authorize(Policy = Permissions.ItemDepartment.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditRecipeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
