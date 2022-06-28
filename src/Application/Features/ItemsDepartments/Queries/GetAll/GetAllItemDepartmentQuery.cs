using AutoMapper;
using ErpDashboard.Application.Features.ItemsDepartments.Queries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;

namespace ErpDashboard.Application.Features.ItemsDepartments.Queries.GetAll
{
    public class GetAllItemDepartmentQuery : IRequest<Result<List<GetItemDepartmentResponse>>>
    {
        public GetAllItemDepartmentQuery()
        {

        }

    }
    internal class GetAllItemDepartmentHandler : IRequestHandler<GetAllItemDepartmentQuery, Result<List<GetItemDepartmentResponse>>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllItemDepartmentHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<GetItemDepartmentResponse>>> Handle(GetAllItemDepartmentQuery request, CancellationToken cancellationToken)
        {
            var depts = await _unitOfWork.Repository<TbDepartment>().GetAllAsync();
            var MappedResult = _mapper.Map<List<GetItemDepartmentResponse>>(depts);
            return await Result<List<GetItemDepartmentResponse>>.SuccessAsync(MappedResult);

        }
    }
}
