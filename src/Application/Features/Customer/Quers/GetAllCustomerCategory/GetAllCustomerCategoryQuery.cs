using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Customer.Quers.GetAllCustomerCategory
{
    public class GetAllCustomerCategoryQuery:IRequest<IResult<List<GetAllCustomerCategoryViewModel>>>
    {
    }
    internal class GetAllCustomerCategoryQueryHandler : IRequestHandler<GetAllCustomerCategoryQuery, IResult<List<GetAllCustomerCategoryViewModel>>>
    {
        private readonly ICustomIUnitOfWork<TbCustomerCategory> _UnitOFWork;
        private readonly IMapper _Mapper;

        public GetAllCustomerCategoryQueryHandler(ICustomIUnitOfWork<TbCustomerCategory> unitOFWork, IMapper mapper)
        {
            _UnitOFWork = unitOFWork;
            _Mapper = mapper;
        }

        public async Task<IResult<List<GetAllCustomerCategoryViewModel>>> Handle(GetAllCustomerCategoryQuery request, CancellationToken cancellationToken)
        {
            var customerCategories = await _UnitOFWork.Repository<TbCustomerCategory>().GetAllAsync();
            var Mapped=_Mapper.Map<List<GetAllCustomerCategoryViewModel>>(customerCategories);
            return await Result<List<GetAllCustomerCategoryViewModel>>.SuccessAsync(Mapped);
        }
    }
}
