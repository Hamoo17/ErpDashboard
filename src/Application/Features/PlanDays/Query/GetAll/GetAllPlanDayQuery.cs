using AutoMapper;
using ErpDashboard.Application.Features.PlanDays.Query.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;

namespace ErpDashboard.Application.Features.PlanDays.Query.GetAll
{
    public class GetAllPlanDayQuery : IRequest<IResult<List<PlanDayDto>>>
    {
    }

    internal class GetAllPlanDayQueryHandler : IRequestHandler<GetAllPlanDayQuery, IResult<List<PlanDayDto>>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllPlanDayQueryHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<List<PlanDayDto>>> Handle(GetAllPlanDayQuery request, CancellationToken cancellationToken)
        {
            var planDay = await _unitOfWork.Repository<TbPlanDay>().GetAllAsync();
            var MappedPlanday = _mapper.Map<List<PlanDayDto>>(planDay);
            return await Result<List<PlanDayDto>>.SuccessAsync(MappedPlanday);
        }


    }
}
