using AutoMapper;
using ErpDashboard.Application.Features.Customer.Quers.GetAllCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Mappings
{
    public class CustomerPhoneClientDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Phone { get; set; }
        public string PhoneType { get; set; }
    }
    //public class CustomerPhoneProfile : Profile
    //{
    //    public CustomerPhoneProfile()
    //    {
    //        CreateMap<CustomerPhoneClientDto, PhonsDto>().ReverseMap();
    //    }
    //}
}
