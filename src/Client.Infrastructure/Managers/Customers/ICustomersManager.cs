﻿using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Shared.Wrapper;

namespace ErpDashboard.Client.Infrastructure.Managers.Customers
{
    public interface ICustomersManager : IManager
    {
        Task<PaginatedResult<GetAllCustomerViewModal>> GetAllAsync(GetAllCustomersQuery Request);
        Task<IResult<int>> SaveAsync(AddEditCustomerCommand Command);
    }
}
