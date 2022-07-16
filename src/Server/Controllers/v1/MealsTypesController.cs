using ErpDashboard.Application.Features.MealsTypes.Commands.AddEdit;
using ErpDashboard.Application.Features.MealsTypes.Commands.Delete;
using ErpDashboard.Application.Features.MealsTypes.Queries.GetAll;
using ErpDashboard.Application.Features.MealsTypes.Queries.GetById;
using ErpDashboard.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErpDashboard.Server.Controllers.v1
{

    public class MealsTypesController : BaseApiController<MealsTypesController>
    {
        /// <summary>
        /// Create/Update Meals Types (AddEditMealsTypesCommand)
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.MealsTypes.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditMealsTypesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Meal Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.MealsTypes.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteMealsTypesCommand { Id = id }));
        }

        /// <summary>
        /// Get All Meal Types
        /// </summary>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.MealsTypes.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var MealsTypes = await _mediator.Send(new GetAllMealsTypesQuery());
            return Ok(MealsTypes);
        }

        /// <summary>
        /// Get Meal Types By Id
        /// </summary>
        /// <param name="id">Id To Get</param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.MealsTypes.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var MealsTypes = await _mediator.Send(new GetMealsTypesByIdQuery() { Id = id });
            return Ok(MealsTypes);
        }
    }
}
