using ErpDashboard.Application.Features.Subscriptions.Queries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpDashboard.Application.Features.Subscriptions.Queries.GetSidByPhone
{
    public class GetSidByPhoneQury : IRequest<IResult<CustomerInfoDto>>
    {
        public string PhoneNumber { get; set; }
    }
    internal class GetSidByPhoneQuryHandler : IRequestHandler<GetSidByPhoneQury, IResult<CustomerInfoDto>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;

        public GetSidByPhoneQuryHandler(ICustomIUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult<CustomerInfoDto>> Handle(GetSidByPhoneQury request, CancellationToken cancellationToken)
        {
            var CustomerId = _unitOfWork.Repository<TbCustomersPhone>().Entities.FirstOrDefault(x => x.Phone == request.PhoneNumber)?.CustomerId;
            if (CustomerId.HasValue)
            {
                var SubscriptionsHdr = await _unitOfWork.Repository<TbCustomer>().Entities.Where(x => x.Id == CustomerId.Value)
                    .Select(x => new CustomerInfoDto()
                    {
                        CustomerId = x.CustomerId,
                        CustomerName = x.CustomerName,
                        Email = x.Email,
                        Phone = String.Join(" | ", x.TbCustomersPhones.Where(p => !string.IsNullOrEmpty(p.Phone)).Select(x => x.Phone)),
                        Subscriptions = x.TbSubscrbtionHeaders.Select(d => new SubscriptionDetailDto()
                        {
                            DeliveryAddres = d.TbSubscrbtionDetails.Select(x => x.DeliveryAdressNavigation.Adress).First(),
                            BranchName = d.Branch.BranchName,
                            Company = d.Com.CompanySymbol,
                            DriverName = d.Driver.DriverName,
                            Duration = d.Durations,
                            StartDate = d.StartDate,
                            Status = d.SubscriptionStatus,
                            SubscriptionId = d.Id,
                            PlanName = d.Plan.PlanName,
                            PlanTitle = d.SubscriptionExepression,
                            RemainingDays = d.TbSubscrbtionDetails.Where(x => x.DeliveryStatus == Enums.ErpSystemEnums.DeliveryStatus.Pending || x.DeliveryStatus == Enums.ErpSystemEnums.DeliveryStatus.Hold).Select(c => c.DayNumberCount).Distinct().Count(),

                        }).ToList()

                    }).FirstOrDefaultAsync();
                return await Result<CustomerInfoDto>.SuccessAsync(SubscriptionsHdr);
            }
            else
            {
                return await Result<CustomerInfoDto>.FailAsync("No Subscription For This Number");
            }
        }
    }
}
