﻿using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Application.Features.PlanCategory.Query.GetAll;
using ErpDashboard.Application.Features.PlanDays.Command.AddEdit;
using ErpDashboard.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAll()
        {
            var Customers = await _mediator.Send(new GetAllCustomersQuery());
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
    }
}