using AutoMapper;
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
using Microsoft.EntityFrameworkCore;

namespace ErpDashboard.Application.Features.Recipe.Queries.GetRecipeByItemId
{
    public class GetRecipeByItemIdQuery : IRequest<Result<List<ItemComponentDetailResponse>>>
    {
        public int ComplexItemId { get; set; }

        public GetRecipeByItemIdQuery()
        {

        }
    }
    internal class GetRecipeByIdHandler : IRequestHandler<GetRecipeByItemIdQuery, Result<List<ItemComponentDetailResponse>>>
    {
        private readonly ICustomIUnitOfWork<int> _customIUnitOfWork;
        private readonly IMapper _mapper;
        public GetRecipeByIdHandler(ICustomIUnitOfWork<int> customIUnitOfWork, IMapper mapper)
        {
            _customIUnitOfWork = customIUnitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<ItemComponentDetailResponse>>> Handle(GetRecipeByItemIdQuery query, CancellationToken cancellationToken)
        {
            var ComplexItem = _customIUnitOfWork.Repository<TbItemComponentsHdr>().Entities.Where(c => c.ComplexItem == query.ComplexItemId)
                .Include(c=>c.TbItemComponentsLines).FirstOrDefault();
            var ItemMapped = _mapper.Map<List<ItemComponentDetailResponse>>(ComplexItem.TbItemComponentsLines);
            return await Result<List<ItemComponentDetailResponse>>.SuccessAsync(ItemMapped);

        }

    }
}
