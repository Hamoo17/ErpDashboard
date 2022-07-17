using AutoMapper;
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

namespace ErpDashboard.Application.Features.Customer.Command.Delete
{
    public class DeleteCustomerCommand:IRequest<IResult<int>>
    {
        public int id { get; set; }
    }
    internal class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, IResult<int>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCustomerCommandHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<int>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var CustomerExisit =await  _unitOfWork.Repository<TbSubscrbtionHeader>().Entities.FirstOrDefaultAsync(x => x.CustomerId == request.id);
            if(CustomerExisit!=null)
            {
                return await Result<int>.FailAsync("Cant Delete Customer.Used in ANother Process");
                
            }
            var Customer = await _unitOfWork.Repository<TbCustomer>().GetByIdAsync(request.id);
            if (Customer != null)
            {
                Customer = _mapper.Map<TbCustomer>(Customer);
                await _unitOfWork.Repository<TbCustomer>().DeleteAsync(Customer);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(Customer.Id, "Customer  Deleted");
            }
            else
                return await Result<int>.FailAsync("Customer not Exisit");
        }
    }
}
