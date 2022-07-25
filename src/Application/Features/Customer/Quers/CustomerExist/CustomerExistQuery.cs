using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Application.Features.Customer.Quers.PhoneExist;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Customer.Quers.CustomerExist
{
    public class CustomerExistQuery: IRequest<IResult<GetAllCustomerViewModal>>
    {
        public string Phone { get; set; }

    }

    internal class CustomerExistQueryHandler : IRequestHandler<CustomerExistQuery, IResult<GetAllCustomerViewModal>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly AutoMapper.Mapper _Mapper;

		public CustomerExistQueryHandler(ICustomIUnitOfWork<int> unitOfWork, AutoMapper.Mapper mapper)
		{
			_unitOfWork = unitOfWork;
			_Mapper = mapper;
		}

		public async Task<IResult<GetAllCustomerViewModal>> Handle(CustomerExistQuery request, CancellationToken cancellationToken)
        {
           var phons= await _unitOfWork.Repository<TbCustomersPhone>().Entities.FirstOrDefaultAsync(x => x.Phone == request.Phone );
            var customer = new TbCustomer();
            if(phons!=null)
			{
                 customer = await _unitOfWork.Repository<TbCustomer>().Entities.FirstOrDefaultAsync(c => c.Id == phons.CustomerId);
                var WrappedCustomer=_Mapper.Map<GetAllCustomerViewModal>(customer);
                return await Result<GetAllCustomerViewModal>.SuccessAsync(WrappedCustomer);
            }
            else
            {
                return await Result<GetAllCustomerViewModal>.SuccessAsync(new GetAllCustomerViewModal());
            }
        }
    }

}
