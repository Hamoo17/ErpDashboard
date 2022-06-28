using ErpDashboard.Application.Features.Items.Commands.AddEdit;
using ErpDashboard.Application.Features.Items.Commands.Delete;
using ErpDashboard.Application.Features.Items.Quaries.GetAll;
using ErpDashboard.Application.Features.Items.Quaries.GetAllComplexCompleteItems;
using ErpDashboard.Application.Features.Items.Quaries.GetById;
using ErpDashboard.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpDashboard.Server.Controllers.v1
{

    public class ItemController : BaseApiController<ItemController>
    {
        /// <summary>
        /// Create/Update Item (AddEditItemCommand)
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Item.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditItemCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Item.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteItemCommand { Id = id }));
        }

        /// <summary>
        /// Get All Items (List of GetItemResponse)
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Item.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Items = await _mediator.Send(new GetAllItemsQuery());
            return Ok(Items);
        }

        /// <summary>
        /// Get Items By Id (GetItemResponse)
        /// </summary>
        /// <param name="id">Id To Get</param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Item.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Items = await _mediator.Send(new GetItemByIdQuery() { Id = id });
            return Ok(Items);
        }

        /// <summary>
        /// Get All Items (List of GetItemResponse)
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Item.View)]
        [HttpGet("GetAllComplete")]
        public async Task<IActionResult> GetAllComplete()
        {
            var Items = await _mediator.Send(new GetAllComlexCompleteItemsQuery());
            return Ok(Items);
        }
    }
}
