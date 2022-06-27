using AutoMapper;
using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Mappings
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<TbCustomer, GetAllCustomerViewModal>().ReverseMap();
            CreateMap<TbCustomer, AddEditCustomerCommand>().ReverseMap();
        }
    }
}
