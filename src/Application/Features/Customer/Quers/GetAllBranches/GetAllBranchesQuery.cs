using AutoMapper;
using ErpDashboard.Application.Features.PlanDays.Query.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Customer.Quers.GetAllBranches
{
    public class GetAllBranchesQuery:IRequest<IResult<List<BranchesDto>>>
    {

    }
    internal class GetAllBranchesQueryHandler : IRequestHandler<GetAllBranchesQuery, IResult<List<BranchesDto>>>
    {
        private readonly ICustomIUnitOfWork<TbErbMainBranch> _UnitOFWork;
        private readonly IMapper _Mapper;

        public GetAllBranchesQueryHandler(ICustomIUnitOfWork<TbErbMainBranch> unitOFWork, IMapper mapper)
        {
            _UnitOFWork = unitOFWork;
            _Mapper = mapper;
        }

        public async Task<IResult<List<BranchesDto>>> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
        {
            var branches = await _UnitOFWork.Repository<TbErbMainBranch>().GetAllAsync();
            var mappedAdress=_Mapper.Map<List<BranchesDto>>(branches);
            return await Result<List<BranchesDto>>.SuccessAsync(mappedAdress);

        }
    }
}
