using ErpDashboard.Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Customer.GetAllCustomers
{
    public class GetAllCustomerViewModal
    {
        public int id { get; set; }
        public int CustomerId { get; set; }
        public string CustCoupon { get; set; }
        public string CustomerName { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public int CustomerType { get; set; }
        public int Evalution { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
        public string CustomerPhone  { get; set; }
        public string  Adress { get; set; }
        public string  Category { get; set; }
    }
}
