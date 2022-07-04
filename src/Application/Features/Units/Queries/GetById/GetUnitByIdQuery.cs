using AutoMapper;
using ErpDashboard.Application.Features.Units.Queries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Units.Queries.GetById
{
    public class GetUnitByIdQuery : IRequest<Result<UnitResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetUnitByIdHandler : IRequestHandler<GetUnitByIdQuery, Result<UnitResponse>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        public GetUnitByIdHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<UnitResponse>> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
        {
            var unit = await _unitOfWork.Repository<TbUnit>().GetByIdAsync(request.Id);
            var UnitMapped = _mapper.Map<UnitResponse>(unit);
            return await Result<UnitResponse>.SuccessAsync(UnitMapped);
        }
    }
}
