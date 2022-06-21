using AutoMapper;
using ErpDashboard.Application.Features.PlanCategory.Query.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.PlanCategory.Query.GetAll
{
    public class GetAllPlanCategoryQuery:IRequest<Result<List<PlanCategoryDto>>>
    {
        public GetAllPlanCategoryQuery()
        {
                
        }
    }

    internal class GetAllPlanCategoryQueryHandler : IRequestHandler<GetAllPlanCategoryQuery, Result<List<PlanCategoryDto>>>
{
        private readonly CustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPlanCategoryQueryHandler(CustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<PlanCategoryDto>>> Handle(GetAllPlanCategoryQuery request, CancellationToken cancellationToken)
        {
            var PlanCategories =await  _unitOfWork.Repository<TbPlanCategory>().GetAllAsync();
            var MappedCategiries = _mapper.Map<List<PlanCategoryDto>>(PlanCategories);
            return await Result<List<PlanCategoryDto>>.SuccessAsync(MappedCategiries);
        }
    }
}
