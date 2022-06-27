using AutoMapper;
using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<TbCustomer, GetAllCustomerViewModal>().ReverseMap();
            CreateMap<TbCustomer, AddEditCustomerCommand>().ReverseMap();
        }
    }
}
