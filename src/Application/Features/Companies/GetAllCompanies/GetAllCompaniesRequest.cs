using ErpDashboard.Application.Features.Companies.GetAllCompanies.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;

namespace ErpDashboard.Application.Features.Companies.GetAllCompanies
{
    public class GetAllCompaniesRequest : IRequest<IResult<List<GetAllCompaniesDto>>>
    {
        public GetAllCompaniesRequest()
        {

        }
    }
    internal class GetAllCompaniesRequestHandler : IRequestHandler<GetAllCompaniesRequest, IResult<List<GetAllCompaniesDto>>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly ICompanyRepository _repository;
        public GetAllCompaniesRequestHandler(ICustomIUnitOfWork<int> unitOfWork, ICompanyRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<IResult<List<GetAllCompaniesDto>>> Handle(GetAllCompaniesRequest request, CancellationToken cancellationToken)
        {

            var Companies = await _unitOfWork.Repository<TbCompany>().GetAllAsync();
            var MappedCompanies = Companies.Select(c => new GetAllCompaniesDto() { CompanyId = c.ComId, CompanyName = c.ComName, CompanySympol = c.CompanySymbol }).ToList();
            return await Result<List<GetAllCompaniesDto>>.SuccessAsync(MappedCompanies);
        }
    }
}
