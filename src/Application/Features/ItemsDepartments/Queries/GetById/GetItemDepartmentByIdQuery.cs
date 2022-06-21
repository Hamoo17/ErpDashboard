using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpDashboard.Application.Features.ItemsDepartments.Queries.Dto;
using MediatR;
using System.Threading;
using ErpDashboard.Application.Interfaces.Repositories;
using AutoMapper;
using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Features.ItemsDepartments.Queries.GetById
{
    internal class GetItemDepartmentByIdQuery : IRequest<Result<GetItemDepartmentResponse>>
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
            var dept = _unitOfWork.Repository<TbDepartment>().GetByIdAsync(request.id);
            var MapedDept =  _mapper.Map<GetItemDepartmentResponse>(dept);
            return await Result<GetItemDepartmentResponse>.Success(MapedDept);
        }
    }
}
