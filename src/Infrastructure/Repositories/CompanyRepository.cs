using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Infrastructure.Contexts;

namespace ErpDashboard.Infrastructure.Repositories
{
    public class CompanyRepository : CustomRepositoryAsync<TbCompany, int>, ICompanyRepository
    {
        private readonly ERBSYSTEMContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

		public CompanyRepository(ERBSYSTEMContext dbContext, ICurrentUserService currentUserService) : base(dbContext, currentUserService)
		{
			_dbContext = dbContext;
			_currentUserService = currentUserService;
		}
		

        public bool IsCompanyNameExist()
        {

            return false;
        }
    }
}
