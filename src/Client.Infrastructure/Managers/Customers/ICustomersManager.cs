using AutoMapper;
using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Application.Features.PlanDays.Query.Dto;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.Customers
{
    public interface ICustomersManager: IManager
    {
        Task<IResult<List<GetAllCustomerViewModal>>> GetAllAsync();
        Task<IResult<int>> SaveAsync(AddEditCustomerCommand Command);
    }
}
