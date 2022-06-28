using AutoMapper;
using ErpDashboard.Application.Features.Items.Quaries.Dto;
using ErpDashboard.Application.Features.Recipe.Queries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Items.Quaries.GetAllComplexCompleteItems
{
    public class GetAllComlexCompleteItemsQuery : IRequest<Result<List<GetItemResponse>>>
    {
        public GetAllComlexCompleteItemsQuery()
        {

        }
    }
    internal class GetAllComlexCompleteItemsHandler : IRequestHandler<GetAllComlexCompleteItemsQuery, Result<List<GetItemResponse>>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllComlexCompleteItemsHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<GetItemResponse>>> Handle(GetAllComlexCompleteItemsQuery request, CancellationToken cancellationToken)
        {
            var CompleteItems = _unitOfWork.Repository<TbItem>().Entities.Where(c => c.ItemType == 4 || c.ItemType == 5).ToList();
            var itemsMapped = _mapper.Map<List<GetItemResponse>>(CompleteItems);
            return await Result<List<GetItemResponse>>.SuccessAsync(itemsMapped);
        }

    }
}
