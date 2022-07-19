using AutoMapper;
using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Application.Features.Customer.Quers.GetAllAreas;
using ErpDashboard.Application.Features.Customer.Quers.GetAllBranches;
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
            CreateMap<TbCustomerAdress, AdressDto>();
            CreateMap<AdressDto, TbCustomerAdress>().ForMember(x=>x.Area , o=> o.Ignore());
            CreateMap<TbCustomersPhone,PhonsDto>().ReverseMap();
            CreateMap<TbArea, GetAllAreaViewModal>().ForMember(x=>x.BranchName , x=>x.MapFrom(a=>a.Branch.BranchName));
            CreateMap<GetAllAreaViewModal, TbArea>();
            CreateMap<TbErbMainBranch, BranchesDto>().ReverseMap();
          
        }
    }
}
