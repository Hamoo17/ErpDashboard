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

namespace ErpDashboard.Application.Features.Units.Queries.GetAll
{
    public class GetAllUnitQuery : IRequest<Result<List<UnitResponse>>>
    {
        public GetAllUnitQuery()
        {

        }
    }
    internal class GetAllUnitQueryHandler : IRequestHandler<GetAllUnitQuery, Result<List<UnitResponse>>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllUnitQueryHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<UnitResponse>>> Handle(GetAllUnitQuery request, CancellationToken cancellationToken)
        {
            var AllUnit = await _unitOfWork.Repository<TbUnit>().GetAllAsync();
            var UnitMapped =  _mapper.Map<List<UnitResponse>>(AllUnit);
            return await Result<List<UnitResponse>>.SuccessAsync(UnitMapped);
        }
    }
}
