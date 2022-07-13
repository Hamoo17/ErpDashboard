using AutoMapper;
using ErpDashboard.Application.Extensions;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace ErpDashboard.Application.Features.Customer.GetAllCustomers
{
	public class GetAllCustomersQuery:IRequest<PaginatedResult<GetAllCustomerViewModal>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...
		public GetAllCustomersQuery(int pageNumber, int pageSize, string searchString, string orderBy)
		{
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
		public GetAllCustomersQuery()
		{

		}
    }
    public class GetAllCustomersQueryHnadler : IRequestHandler<GetAllCustomersQuery, PaginatedResult<GetAllCustomerViewModal>>
    {
        private readonly ICustomIUnitOfWork<TbCustomer> _UnitOFWork;
        private readonly IMapper _Mapper;

        public GetAllCustomersQueryHnadler(ICustomIUnitOfWork<TbCustomer> unitOFWork, IMapper mapper)
        {
            _UnitOFWork = unitOFWork;
            _Mapper = mapper;
        }

        public async Task<PaginatedResult<GetAllCustomerViewModal>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<TbCustomer, bool>> expression =x=> true;
			if (!string.IsNullOrEmpty(request.SearchString))
			{
                expression = x => x.CustomerName.Contains(request.SearchString) || x.TbCustomersPhones.Select(p => p.Phone).Contains(request.SearchString);

            }
            var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
            var Customers = await _UnitOFWork.Repository<TbCustomer>().Entities.Where(expression).Select(x => new GetAllCustomerViewModal()
            {
                id = x.Id,
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                Email = x.Email,
                CustomerPhone = String.Join('|', x.TbCustomersPhones.Where(p => !string.IsNullOrEmpty(p.Phone)).Select(x => x.Phone)),
                Adress = x.TbCustomerAdresses.FirstOrDefault().Adress.Substring(0, 3),
                Status = x.Status,
                Category=x.Category.CategoryName,
                RegDate=x.RegDate,
                Weight=x.Weight,
                Height=x.Height,
                Notes=x.Notes,
                BirthDate=x.BirthDate,
                CustomerType=x.CustomerType,
                CategoryId=x.CategoryId,
                RegType=x.RegType,
                customerAdresses=x.TbCustomerAdresses.ToList()
            }).OrderBy(ordering).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return Customers;
        }
    }
}
