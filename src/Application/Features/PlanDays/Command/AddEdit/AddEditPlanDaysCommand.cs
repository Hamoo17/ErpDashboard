using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.PlanDays.Command.AddEdit
{
    public class AddEditPlanDaysCommand:IRequest<IResult<int>>
    {
        public int Id { get; set; }
       [Required]
        public string Name { get; set; }
        [Required]
        public int DayCount { get; set; }
    }
    internal class AddEditPlanDaysCommandHandelr : IRequestHandler<AddEditPlanDaysCommand, IResult<int>>
    {
        private readonly CustomIUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public AddEditPlanDaysCommandHandelr(CustomIUnitOfWork<int> unitOfWork, ICurrentUserService currentUser, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<IResult<int>> Handle(AddEditPlanDaysCommand request, CancellationToken cancellationToken)
        {
            if(request.Id==0)
            {
                var planday =  _mapper.Map<TbPlanDay>(request);
                planday.CompanyId = _currentUser.CompanyID.Value;
                await _unitOfWork.Repository<TbPlanDay>().AddAsync(planday);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(planday.Id, "PlanDay Add Successfuly");
            }
            else
            {
                var planday = await _unitOfWork.Repository<TbPlanDay>().GetByIdAsync(request.Id);
                if(planday!=null)
                {
                    planday=_mapper.Map<TbPlanDay>(request);
                    await _unitOfWork.Repository<TbPlanDay>().UpdateAsync(planday,request.Id);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(planday.Id, "PlanDay Updated");
                }else
                    return await Result<int>.FailAsync("PlanDay Not Exist");
            }

        }
}
}
