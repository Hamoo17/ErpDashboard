using ErpDashboard.Application.Features.ItemsDepartments.Commands.AddEdit;
using ErpDashboard.Application.Features.ItemsDepartments.Commands.Delete;
using ErpDashboard.Application.Features.ItemsDepartments.Queries.GetAll;
using ErpDashboard.Application.Features.ItemsDepartments.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace ErpDashboard.Server.Controllers.v1
{

    public class ItemDepartmentController : BaseApiController<ItemDepartmentController>
    {
        /// <summary>
        /// Create/Update Item Department
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
       // [Authorize(Policy = Permissions.ItemDepartment.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditItemDepartmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        /// <summary>
        /// Delete item department
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
      //  [Authorize(Policy = Permissions.ItemDepartment.Create)]
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteItemDepartmentCommand { Id = id }));
        }
        /// <summary>
        /// Get item department By Id
        /// </summary>
        /// <param name="id">Id To Get</param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.ItemDepartment.View)]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetItemDepartmentByIdQuery { id = id }));
        }
        /// <summary>
        /// Get all item department
        /// </summary>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.ItemDepartment.View)]
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllItemDepartmentQuery()));
        }
    }
}
