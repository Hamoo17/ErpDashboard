using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Infrastructure.Contexts;

namespace ErpDashboard.Infrastructure.Repositories
{
    public class CompanyRepository : CustomRepositoryAsync<TbCompany, int>, ICompanyRepository
    {
        private readonly ERBSYSTEMContext _dbContext;

        public CompanyRepository(ERBSYSTEMContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsCompanyNameExist()
        {

            return false;
        }
    }
}
