using ErpDashboard.Application.Features.Companies.GetAllCompanies.Dto;
using ErpDashboard.Shared.Wrapper;

namespace ErpDashboard.Client.Infrastructure.Managers.Compaies
{
    public interface ICompanyManager : IManager
    {
        Task<IResult<List<GetAllCompaniesDto>>> GetAllAsync();
    }
}
