using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ErpDashboard.Application.Enums.ErpSystemEnums;

namespace ErpDashboard.Application.Features.Customer.Command.AddEdit
{
    public class AddEditCustomerCommand:IRequest<IResult<int>>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public Customer_Type CustomerType { get; set; }
        public int Evalution { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
        public int ComId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string RegType { get; set; }
    }
    internal class AddEditCustomerCommandHandler : IRequestHandler<AddEditCustomerCommand, IResult<int>>
    {
        private readonly ICustomIUnitOfWork<int> _UnitOfWork;
        private readonly IMapper _Mapper;
        private readonly ICurrentUserService _currentUser;

        public AddEditCustomerCommandHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<IResult<int>> Handle(AddEditCustomerCommand request, CancellationToken cancellationToken)
        {
           if(request.Id==0)
            {
                var mappedcustomer=_Mapper.Map<TbCustomer>(request);
                mappedcustomer.UserId = _currentUser.SystemUserId.Value;
                mappedcustomer.ComId =_currentUser.CompanyID.Value;
                await _UnitOfWork.Repository<TbCustomer>().AddAsync(mappedcustomer);
                await _UnitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(mappedcustomer.Id,"Customer Add Successfuly");
            }
            else
            {
                var Customer = await _UnitOfWork.Repository<TbCustomer>().GetByIdAsync(request.Id);
               if(Customer!=null)
                {
                    await _UnitOfWork.Repository<TbCustomer>().UpdateAsync(Customer, request.Id);
                    await _UnitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(Customer.Id, "Customer Update Successfuly");
                }
                else
                {
                    return await Result<int>.FailAsync("Customer Not Found");
                }
            }
        }
    }
}
