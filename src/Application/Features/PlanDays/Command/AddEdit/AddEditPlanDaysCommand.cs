using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace ErpDashboard.Application.Features.PlanDays.Command.AddEdit
{
    public class AddEditPlanDaysCommand : IRequest<IResult<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int DayCount { get; set; }
    }
    internal class AddEditPlanDaysCommandHandelr : IRequestHandler<AddEditPlanDaysCommand, IResult<int>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditPlanDaysCommandHandelr> _localizer;

        public AddEditPlanDaysCommandHandelr(ICustomIUnitOfWork<int> unitOfWork, ICurrentUserService currentUser, IMapper mapper, IStringLocalizer<AddEditPlanDaysCommandHandelr> localizer)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<IResult<int>> Handle(AddEditPlanDaysCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var planday = _mapper.Map<TbPlanDay>(request);
                planday.CompanyId = _currentUser.CompanyID.Value;
                await _unitOfWork.Repository<TbPlanDay>().AddAsync(planday);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(planday.Id, "PlanDay Add Successfuly");
            }
            else
            {
                var planday = await _unitOfWork.Repository<TbPlanDay>().GetByIdAsync(request.Id);
                if (planday != null)
                {
                    planday.DayCount = request.DayCount;
                    planday.Name = request.Name ?? planday.Name;
                    await _unitOfWork.Repository<TbPlanDay>().UpdateAsync(planday, request.Id);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(planday.Id, "PlanDay Updated");
                }
                else
                    return await Result<int>.FailAsync("PlanDay Not Exist");
            }
        }
    }
}
