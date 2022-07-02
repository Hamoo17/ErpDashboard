using ErpDashboard.Application.Features.Categories.Command.AddEdit;
using ErpDashboard.Application.Features.Categories.Command.Delete;
using ErpDashboard.Application.Features.Categories.Queries.GetAll;
using ErpDashboard.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErpDashboard.Server.Controllers.v1
{
    [ApiController]
    public class CategoryController : BaseApiController<CategoryController>
    {
        /// <summary>
        /// Create/Update a  Category
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Category.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        /// <summary>
        /// Delete  Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Category.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryCommand { Id = id }));
        }
        /// <summary>
        /// Get All Items (List of CategoryResponse)
        /// </summary>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.Category.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Items = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(Items);
        }
    }
}
