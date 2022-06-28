using AutoMapper;
using ErpDashboard.Application.Features.Items.Quaries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;

namespace ErpDashboard.Application.Features.Items.Quaries.GetAll
{
    public class GetAllItemsQuery : IRequest<Result<List<GetItemResponse>>>
    {
        public GetAllItemsQuery()
        {

        }
    }
    internal class GetAllItemsHandler : IRequestHandler<GetAllItemsQuery, Result<List<GetItemResponse>>>
    {
        private readonly ICustomIUnitOfWork<int> _customIUnitOfWork;
        private readonly IMapper _mapper;
        public GetAllItemsHandler(ICustomIUnitOfWork<int> customIUnitOfWork, IMapper mapper)
        {
            _customIUnitOfWork = customIUnitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<GetItemResponse>>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _customIUnitOfWork.Repository<TbItem>().GetAllAsync();
            var itemsmapped = _mapper.Map<List<GetItemResponse>>(items);
            return await Result<List<GetItemResponse>>.SuccessAsync(itemsmapped);
        }
    }
}
