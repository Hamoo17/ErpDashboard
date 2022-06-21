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

namespace ErpDashboard.Application.Features.PlanCategory.Command.AddEdit
{
    public class AddEditPlanCategoryCommand : IRequest<IResult<int>>
    {
        public int Id { get; set; }
        [Required]
        public string TypeName
        {
            get; set;
        }
        [Required]
        public string Symbol { get; set; }
    }
    internal class AddEditPlanCategoryCommandHandler : IRequestHandler<AddEditPlanCategoryCommand, IResult<int>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public AddEditPlanCategoryCommandHandler(ICustomIUnitOfWork<int> unitOfWork, ICurrentUserService currentUser, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<IResult<int>> Handle(AddEditPlanCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) 
            {
                var plancategory = _mapper.Map<TbPlanCategory>(request);
                plancategory.ComId = _currentUser.CompanyID.Value;
                plancategory.UserId = _currentUser.SystemUserId.Value;
               await _unitOfWork.Repository<TbPlanCategory>().AddAsync(plancategory);
               await  _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(plancategory.Id,"Plan Category Added");
               
            }
            else
            {
                var plancategory= await _unitOfWork.Repository<TbPlanCategory>().GetByIdAsync(request.Id);
                if(plancategory != null)
                {
                    //update
                    plancategory = _mapper.Map<TbPlanCategory>(request);
                    await _unitOfWork.Repository<TbPlanCategory>().UpdateAsync(plancategory, request.Id);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(plancategory.Id, "Plan Category Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Plan Category Not Exist");
                }
            }
        }
    }
}
