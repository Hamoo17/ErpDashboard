using ErpDashboard.Application.Features.Recipe.Commabds.AddEdit;
using ErpDashboard.Application.Features.Recipe.Queries.GetRecipeByItemId;
using ErpDashboard.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Policy = Permissions.Recipe.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditRecipeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        /// <summary>
        /// Get Items Recipy Details By Item Id (ItemComponentHdrResponse)
        /// </summary>
        /// <param name="ComplexItemId">Id To Get</param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.Recipe.View)]
        [HttpGet("{ComplexItemId}")]
        public async Task<IActionResult> GetById(int ComplexItemId)
        {
            var Items = await _mediator.Send(new GetRecipeByItemIdQuery() { ComplexItemId = ComplexItemId });
            return Ok(Items);
        }
    }
}
