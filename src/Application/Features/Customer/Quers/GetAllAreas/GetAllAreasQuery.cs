using AutoMapper;
using ErpDashboard.Application.Features.Customer.Quers.GetAllCustomerCategory;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Customer.Quers.GetAllAreas
{
    public class GetAllAreasQuery:IRequest< IResult<List<GetAllAreaViewModal>>>
    {

    }
    internal class GetAllAreasQueryHandler : IRequestHandler<GetAllAreasQuery, IResult<List<GetAllAreaViewModal>>>
    {

        private readonly ICustomIUnitOfWork<TbCustomerCategory> _UnitOFWork;
        private readonly IMapper _Mapper;

        public GetAllAreasQueryHandler(ICustomIUnitOfWork<TbCustomerCategory> unitOFWork, IMapper mapper)
        {
            _UnitOFWork = unitOFWork;
            _Mapper = mapper;
        }

        public async Task<IResult<List<GetAllAreaViewModal>>> Handle(GetAllAreasQuery request, CancellationToken cancellationToken)
        {
            var Areas = await _UnitOFWork.Repository<TbArea>().GetAllAsync();
            var Mapped = _Mapper.Map<List<GetAllAreaViewModal>>(Areas);
            return await Result<List<GetAllAreaViewModal>>.SuccessAsync(Mapped);
        }
    }
}
