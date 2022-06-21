using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.PlanDays.Command.Delete
{
    public class DeletePlanDayCommand:IRequest<IResult<int>>
    {
        public int ID { get; set; }
    }
    internal class DeletePlanDayCommandHnadler : IRequestHandler<DeletePlanDayCommand, IResult<int>>
    {

        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePlanDayCommandHnadler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<int>> Handle(DeletePlanDayCommand request, CancellationToken cancellationToken)
        {
            var PLANDAY = await _unitOfWork.Repository<TbPlanDay>().GetByIdAsync(request.ID);
            if (PLANDAY != null)
            {
                PLANDAY=_mapper.Map<TbPlanDay>(PLANDAY);    
                await _unitOfWork.Repository<TbPlanDay>().DeleteAsync(PLANDAY);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(PLANDAY.Id,"Planday Deleted");
            }
            else
                return await Result<int>.FailAsync("PlanDay Not Exisit");
        }
    }
}
