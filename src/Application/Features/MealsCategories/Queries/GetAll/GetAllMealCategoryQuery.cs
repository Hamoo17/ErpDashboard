﻿using AutoMapper;
using ErpDashboard.Application.Features.MealsCategory.Queries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Constants.Application;
using ErpDashboard.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        private readonly IAppCache _cashe;
        public GetAllMealCategoryQueryHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cash)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cashe = cash;
        }
        public async Task<Result<List<GetMealCategoryResponse>>> Handle(GetAllMealCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Repository<TbMealsCategory>().GetAllAsync();
            var mappedCates = _mapper.Map<List<GetMealCategoryResponse>>(categories);
            return await Result<List<GetMealCategoryResponse>>.SuccessAsync(mappedCates);
        }
    }
}
