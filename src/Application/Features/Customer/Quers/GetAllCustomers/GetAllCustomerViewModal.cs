using ErpDashboard.Application.Models;
using static ErpDashboard.Application.Enums.ErpSystemEnums;

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
        public Customer_Type CustomerType { get; set; }
        public int Evalution { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
        public string CustomerPhone { get; set; }
        public string Adress { get; set; }
        public string Category { get; set; }
        public string RegType { get; set; }
        public int CategoryId { get; set; }
        public List<TbCustomerAdress>  customerAdresses { get; set; }
    }
}
