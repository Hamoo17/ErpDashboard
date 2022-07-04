using ErpDashboard.Application.Features.Units.Commands.AddEdit;
using ErpDashboard.Application.Features.Units.Commands.Delete;
using ErpDashboard.Application.Features.Units.Queries.Dto;
using ErpDashboard.Application.Features.Units.Queries.GetAll;
using ErpDashboard.Application.Features.Units.Queries.GetById;
using ErpDashboard.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErpDashboard.Server.Controllers.v1
{
     
    public class UnitsController : BaseApiController<UnitsController>
    {
        /// <summary>
        /// Create/Update Unit (AddEditUnitsCommand)
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Units.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditUnitsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        /// <summary>
        /// Delete Unit
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.Units.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteUnitsCommand { Id = id }));
        }
        /// <summary>
        /// Get All Units (List of UnitResponse)
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Units.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Items = await _mediator.Send(new GetAllUnitQuery());
            return Ok(Items);
        }

        /// <summary>
        /// Get Units By Id (UnitResponse)
        /// </summary>
        /// <param name="id">Id To Get</param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Units.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Items = await _mediator.Send(new GetUnitByIdQuery() { Id = id });
            return Ok(Items);
        }
    }
}
