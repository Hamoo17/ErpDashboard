using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Categories.Queries.GetAll
{
    public class GetAllCategoriesQuery : IRequest<Result<List<CategoryResponse>>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    internal class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery , Result<List<CategoryResponse>>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCategoriesQueryHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Repository<TbCategory>().GetAllAsync();
            var CatMapped =  _mapper.Map<List<CategoryResponse>>(categories);
            return await Result<List<CategoryResponse>>.SuccessAsync(CatMapped);

        }
    }
}
