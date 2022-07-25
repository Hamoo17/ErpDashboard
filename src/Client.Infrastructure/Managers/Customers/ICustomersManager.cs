using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Application.Features.Customer.Quers.GetAllAreas;
using ErpDashboard.Application.Features.Customer.Quers.GetAllBranches;
using ErpDashboard.Application.Features.Customer.Quers.GetAllCustomerCategory;
using ErpDashboard.Application.Features.Customer.Quers.GetAllCustomers;
using ErpDashboard.Client.Infrastructure.Mappings;
using ErpDashboard.Shared.Wrapper;

namespace ErpDashboard.Client.Infrastructure.Managers.Customers
{
    public interface ICustomersManager : IManager
    {
        Task<PaginatedResult<GetAllCustomerViewModal>> GetAllAsync(GetAllCustomersQuery Request);
        Task<IResult<int>> SaveAsync(AddEditCustomerCommand Command);
        Task<IResult<List<GetAllCustomerCategoryViewModel>>> GetAllCustomerCategoryAsync();
        Task<IResult<List<GetAllAreaViewModal>>> GetAllAreasAsync();
        Task<bool> IsPhoneExist(int CustomerId, string Phone);
        PhonsDto GetPhoneDto(CustomerPhoneClientDto ClientDto);
        Task<IResult<int>> DeleteAsync(int id);
        Task<IResult<List<BranchesDto>>> GetAllBranchiesAsync();
        Task<IResult<GetAllCustomerViewModal>> IsCustomerExist(string Phone);
    }
}
