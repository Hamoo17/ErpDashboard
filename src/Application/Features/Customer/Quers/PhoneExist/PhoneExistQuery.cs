using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Customer.Quers.PhoneExist
{
    public class PhoneExistQuery : IRequest<bool>
    {
        public int CustomerId { get; set; }
        public string Phone { get; set; }
    }

    internal class PhoneExistQueryHandler : IRequestHandler<PhoneExistQuery, bool>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;

        public PhoneExistQueryHandler(ICustomIUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(PhoneExistQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<TbCustomersPhone>().Entities.AnyAsync(x => x.Phone == request.Phone && x.CustomerId != request.CustomerId);
        }
    }
}
