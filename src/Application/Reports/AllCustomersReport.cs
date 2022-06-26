using ErpDashboard.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Reports
{
   public class AllCustomersReport 
    {
        ICustomIUnitOfWork<int> _unitofWork;
        public AllCustomersReport(ICustomIUnitOfWork<int> unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public  List<TbCustomer> CustomersList() 
        {
            return _unitofWork.Repository<TbCustomer>().Entities.ToList();
        }
    }
}
