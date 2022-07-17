using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.Command.Delete;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Application.Features.Customer.Quers.GetAllAreas;
using ErpDashboard.Application.Features.Customer.Quers.GetAllCustomerCategory;
using ErpDashboard.Application.Features.Customer.Quers.PhoneExist;
using ErpDashboard.Application.Features.PlanDays.Command.Delete;
using ErpDashboard.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;

namespace ErpDashboard.Server.Controllers.v1
{
    public class CustomerController : BaseApiController<CustomerController>
    {
        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.PlanCategory.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var Customers = await _mediator.Send(new GetAllCustomersQuery(pageNumber,pageSize,searchString,orderBy));
            return Ok(Customers);
        }
        /// <summary>
        /// Create/Update a Customers
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.planday.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditCustomerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// Get All CustomerCategories
        /// </summary>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.PlanCategory.View)]
        [HttpGet("GetAllCustomerCatrgories")]
        public async Task<IActionResult> GetAllCustomerCatrgories()
        {
            var Customers = await _mediator.Send(new GetAllCustomerCategoryQuery());
            return Ok(Customers);
        }

        /// Get All Areas
        /// </summary>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.PlanCategory.View)]
        [HttpGet("GetAllAreas")]
        public async Task<IActionResult> GetAllAreas()
        {
            var Areas = await _mediator.Send(new GetAllAreasQuery());
            return Ok(Areas);
        }

        /// Check If Phonenumber Exist
        /// </summary>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.PlanCategory.View)]
        [HttpGet("isPhoneExist/{id}/{Phone}")]
        public async Task<IActionResult> isPhoneExist(int id, string Phone)
        {
            var Areas = await _mediator.Send(new PhoneExistQuery() {CustomerId = id ,Phone = Phone });
            return Ok(Areas);
        }
        /// <summary>
        /// Delete a Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.planday.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteCustomerCommand { id = id }));
        }

    }
}
