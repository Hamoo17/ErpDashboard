using AutoMapper;
using ErpDashboard.Application.Features.PlanCategory.Query.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;

namespace ErpDashboard.Application.Features.PlanCategory.Query.GetAll
{
    public class GetAllPlanCategoryQuery : IRequest<Result<List<customercategoryviewmodel>>>
    {
        public GetAllPlanCategoryQuery()
        {

        }
    }

    internal class GetAllPlanCategoryQueryHandler : IRequestHandler<GetAllPlanCategoryQuery, Result<List<customercategoryviewmodel>>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPlanCategoryQueryHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<customercategoryviewmodel>>> Handle(GetAllPlanCategoryQuery request, CancellationToken cancellationToken)
        {
            var PlanCategories = await _unitOfWork.Repository<TbPlanCategory>().GetAllAsync();
            var MappedCategiries = _mapper.Map<List<customercategoryviewmodel>>(PlanCategories);
            return await Result<List<customercategoryviewmodel>>.SuccessAsync(MappedCategiries);
        }
    }
}
