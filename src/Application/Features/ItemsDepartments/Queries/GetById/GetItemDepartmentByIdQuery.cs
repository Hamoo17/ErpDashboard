using AutoMapper;
using ErpDashboard.Application.Features.ItemsDepartments.Queries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;

namespace ErpDashboard.Application.Features.ItemsDepartments.Queries.GetById
{
    public class GetItemDepartmentByIdQuery : IRequest<Result<GetItemDepartmentResponse>>
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    internal class GetItemDepartmentByIdQueryHandler : IRequestHandler<GetItemDepartmentByIdQuery, Result<GetItemDepartmentResponse>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        public GetItemDepartmentByIdQueryHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<GetItemDepartmentResponse>> Handle(GetItemDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var dept = await _unitOfWork.Repository<TbDepartment>().GetByIdAsync(request.id);
            var MapedDept = _mapper.Map<GetItemDepartmentResponse>(dept);
            return await Result<GetItemDepartmentResponse>.SuccessAsync(MapedDept);
        }
    }
}
