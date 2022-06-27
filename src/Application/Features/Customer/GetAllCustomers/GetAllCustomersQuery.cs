using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpDashboard.Application.Features.Customer.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<IResult<List<GetAllCustomerViewModal>>>
    {

    }
    public class GetAllCustomersQueryHnadler : IRequestHandler<GetAllCustomersQuery, IResult<List<GetAllCustomerViewModal>>>
    {
        private readonly ICustomIUnitOfWork<TbCustomer> _UnitOFWork;
        private readonly IMapper _Mapper;

        public GetAllCustomersQueryHnadler(ICustomIUnitOfWork<TbCustomer> unitOFWork, IMapper mapper)
        {
            _UnitOFWork = unitOFWork;
            _Mapper = mapper;
        }

        public async Task<IResult<List<GetAllCustomerViewModal>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var Customers = await _UnitOFWork.Repository<TbCustomer>().Entities.Include(x => x.TbCustomersPhones)
                .Include(x => x.TbCustomerAdresses).Include(x => x.Category).ToListAsync();
            // var MappedCustomers = _Mapper.Map<List<GetAllCustomerViewModal>>(Customers);
            var MappedCustomers = Customers.Select(x => new GetAllCustomerViewModal()
            {
                id = x.Id,
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                Email = x.Email,
                CustomerPhone = String.Join('|', x.TbCustomersPhones.Where(p => !string.IsNullOrEmpty(p.Phone)).Select(x => x.Phone)),
                Adress = x.TbCustomerAdresses.FirstOrDefault().Adress.Substring(0, 3),
                Status = x.Status,
                Category = x.Category.CategoryName,
                RegDate = x.RegDate,
            }).ToList();
            return await Result<List<GetAllCustomerViewModal>>.SuccessAsync(MappedCustomers);
        }
    }
}
