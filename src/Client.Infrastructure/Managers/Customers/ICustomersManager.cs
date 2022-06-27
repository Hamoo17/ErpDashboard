using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Shared.Wrapper;

namespace ErpDashboard.Client.Infrastructure.Managers.Customers
{
    public interface ICustomersManager : IManager
    {
        Task<IResult<List<GetAllCustomerViewModal>>> GetAllAsync();
        Task<IResult<int>> SaveAsync(AddEditCustomerCommand Command);
    }
}
