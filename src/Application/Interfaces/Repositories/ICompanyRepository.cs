using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Interfaces.Repositories
{
    public interface ICompanyRepository : CustomIRepositoryAsync<TbCompany, int>
    {
        bool IsCompanyNameExist();
    }
}
