using AutoMapper;
using ErpDashboard.Application.Features.MealsCategory.Queries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpDashboard.Application.Features.MealsCategory.Queries.GetAll
{
    public class GetAllMealCategoryQuery : IRequest<Result<List<MealsTypesResponse>>>
    {
        public GetAllMealCategoryQuery()
        {

        }
    }
    internal class GetAllMealCategoryQueryHandler : IRequestHandler<GetAllMealCategoryQuery, Result<List<MealsTypesResponse>>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _userService;
        public GetAllMealCategoryQueryHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper, ICurrentUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<Result<List<MealsTypesResponse>>> Handle(GetAllMealCategoryQuery request, CancellationToken cancellationToken)
        {
            //var categories = await _unitOfWork.Repository<TbMealsCategory>().GetAllAsync();
            var mappedCates = await _unitOfWork.Repository<TbMealsType>().Entities.Where(c => c.ComId == _userService.CompanyID)
                .Select(c => new MealsTypesResponse() //MealsTypesResponse
                {
                    CategoryId = c.CategoryId,
                    Id = c.Id,
                    Name = c.Name,
                    CategoryName = c.Category.EnName,
                    CategorySympol = c.Category.Symbol
                }).ToListAsync();
               
            //var mappedCates = _mapper.Map<List<GetMealCategoryResponse>>(categories);
            return await Result<List<MealsTypesResponse>>.SuccessAsync(mappedCates);

        }
    }
}
