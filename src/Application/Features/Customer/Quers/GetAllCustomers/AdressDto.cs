using ErpDashboard.Application.Features.Customer.Quers.GetAllAreas;
using ErpDashboard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Customer.Quers.GetAllCustomers
{
    public class AdressDto
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public int CustomerId { get; set; }
        public string Adress { get; set; }
        public GetAllAreaViewModal Area { get; set; }
    }
}
