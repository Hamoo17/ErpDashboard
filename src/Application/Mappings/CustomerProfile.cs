using AutoMapper;
using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Application.Features.Customer.Quers.GetAllCustomerCategory;
using ErpDashboard.Application.Features.Customer.Quers.GetAllCustomers;
using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<TbCustomer, GetAllCustomerViewModal>().ReverseMap();
            CreateMap<TbCustomer, AddEditCustomerCommand>().ReverseMap();
            CreateMap<TbCustomerCategory, GetAllCustomerCategoryViewModel>().ReverseMap();
            CreateMap<TbCustomerAdress, AdressDto>().ReverseMap();
            CreateMap<TbCustomersPhone,PhonsDto>().ReverseMap();
        }
    }
}
