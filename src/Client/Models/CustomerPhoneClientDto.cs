using ErpDashboard.Client.Infrastructure.Managers.Customers;
using ErpDashboard.Client.Infrastructure.Mappings;
using FluentValidation;

namespace ErpDashboard.Client.Models
{
  
    public class CustomerPhoneClientDtoValidator : AbstractValidator<CustomerPhoneClientDto> 
    {
        private readonly ICustomersManager _manager;
        public CustomerPhoneClientDtoValidator(ICustomersManager manager)
        {
            _manager = manager;
           // RuleFor(x => x).MustAsync(async (x, y) => await IsPhoneExist(x.Phone, x.CustomerId));
            RuleFor(x => x.PhoneType).NotEmpty().NotNull().WithMessage("Phone Type Can Not be Empty");
            RuleFor(x => x.Phone).NotEmpty().NotNull().WithMessage("Phone Number Can Not be Empty").Length(10).WithMessage("Phone Length Must be 10.");
           
            When(z => z.Phone?.Length == 10, () => 
            {
                RuleFor(x => x.Phone).MustAsync(async (y, z) =>
               {
                   return await IsPhoneExist(y);
               }).WithMessage("THIS PHONE NUMBER ALREADY Exist");
            });
             
        }
       
        private async Task<bool> IsPhoneExist(string Phone , int CustomerId=0) 
        {
            if (!string.IsNullOrEmpty(Phone))
            {
                var Result = await _manager.IsPhoneExist(CustomerId, Phone);
                return !Result;
            }
            return await Task.FromResult(false);
        }

    }
}
