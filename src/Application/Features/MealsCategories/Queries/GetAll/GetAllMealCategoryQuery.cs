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
    public class GetAllMealCategoryQuery : IRequest<Result<List<GetMealCategoryResponse>>>
    {
        public GetAllMealCategoryQuery()
        {

        }
    }
    internal class GetAllMealCategoryQueryHandler : IRequestHandler<GetAllMealCategoryQuery, Result<List<GetMealCategoryResponse>>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _userService;
        public GetAllMealCategoryQueryHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper,  ICurrentUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<Result<List<GetMealCategoryResponse>>> Handle(GetAllMealCategoryQuery request, CancellationToken cancellationToken)
        {
            //var categories = await _unitOfWork.Repository<TbMealsCategory>().GetAllAsync();
            var mappedCates = await _unitOfWork.Repository<TbMealsCategory>().Entities.Where(c => c.ComId == _userService.CompanyID)
                .Select(c => new GetMealCategoryResponse()
                {
                    EnName = c.EnName,
                    Id = c.Id,
                    IsSnack = c.Issnack,
                    Symbol = c.Symbol,
                    tbMealsTypes = c.TbMealsTypes.ToList()
                }).ToListAsync();
            //var mappedCates = _mapper.Map<List<GetMealCategoryResponse>>(categories);
            return await Result<List<GetMealCategoryResponse>>.SuccessAsync(mappedCates);

        }
    }
}
