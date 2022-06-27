using ErpDashboard.Application.Features.MealCategory.Commands.Delete;
using ErpDashboard.Application.Features.MealsCategory.Commands.AddEdit;
using ErpDashboard.Application.Features.MealsCategory.Queries.GetAll;
using ErpDashboard.Application.Features.MealsCategory.Queries.GetById;
using ErpDashboard.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpDashboard.Server.Controllers.v1
{

    public class MealCategoriesController : BaseApiController<MealCategoriesController>
    {
        /// <summary>
        /// Create/Update a Meal Category
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MealCategories.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditMealsCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Meal Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MealCategories.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteMealCategoryCommand { Id = id }));
        }

        /// <summary>
        /// Get All Meal Categories
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MealCategories.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var MealCategories = await _mediator.Send(new GetAllMealCategoryQuery());
            return Ok(MealCategories);
        }

        /// <summary>
        /// Get Meal Categories By Id
        /// </summary>
        /// <param name="id">Id To Get</param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MealCategories.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var MealCategory = await _mediator.Send(new GetMealCategoryByIdQuery() { Id = id });
            return Ok(MealCategory);
        }
    }
}
