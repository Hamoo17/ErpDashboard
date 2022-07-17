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
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone Number Can Not be Empty")
                .MustAsync(async (y, z) =>
           {
               return await IsPhoneExist(y);
           })
                // .Must((x,y) => IsPhoneExist2(x.Phone,x.CustomerId))
                .WithMessage("THIS PHONE NUMBER ALREADY Exist");
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

        //private bool IsPhoneExist2(string Phone, int CustomerId = 0)
        //{
        //    if (!string.IsNullOrEmpty(Phone))
        //    {
        //        var r = Task.Run(async () => 
        //        {
        //             return ! await _manager.IsPhoneExist(CustomerId, Phone);
        //        });
        //    }
        //    return false;
        //}
    }
}
